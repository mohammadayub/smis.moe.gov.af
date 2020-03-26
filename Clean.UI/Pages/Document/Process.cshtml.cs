using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.ProcessTrackings.Commands;
using Clean.Application.ProcessTrackings.Models;
using Clean.Application.ProcessTrackings.Queries;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Document
{
    public class ProcessModel : BasePage
    {
        [BindProperty]
        public string LogicID { get; set; }
        

        public async Task<IActionResult> OnGetAsync([FromQuery] int? recordid = null)
        {

            try
            {
                
                ListOfProcessesConnection = new List<SelectListItem>();
                int ScreenID;

                ScreenID = int.TryParse(HttpContext.Request.Query["p"], out ScreenID) ? ScreenID : Convert.ToInt32(EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]));


                var ProcessConnections = await Mediator.Send(new GetProcessConnection() { ScreenId = ScreenID});

                var Processes = await Mediator.Send(new GetProcess() { ScreenId = ScreenID });

                if (ProcessConnections.Any())
                {
                    foreach (var PC in ProcessConnections)
                    {
                        ListOfProcessesConnection.Add(new SelectListItem(PC.ConnectionText, PC.ConnectionId.ToString()));
                    }
                    if (Processes.Any())
                    {
                        var first = Processes.FirstOrDefault();
                        HttpContext.Session.SetInt32("ModuleID", first.ModuleId);
                        HttpContext.Session.SetInt32("ProcessID", first.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }

            return Page();

        }


        public async Task<IActionResult> OnPostSave([FromBody] SaveProcessTracksCommand command)
        {
            try
            {
                var QueryResult = await Mediator.Send(new SearchProcessTrackQuery() { RecordId = command.RecordId, ModuleId =Convert.ToInt16(HttpContext.Session.GetInt32("ModuleID").Value) });
                var CurrentProcess = QueryResult.FirstOrDefault();

                if (command.Id == CurrentProcess.Id && CurrentProcess.ProcessId == HttpContext.Session.GetInt32("ProcessID"))
                {
                    command.ModuleId = CurrentProcess.ModuleId;
                    QueryResult = await Mediator.Send(command);
                    return new JsonResult(new UIResult()
                    {
                        Data = new { list = QueryResult },
                        Status = UIStatus.Success,
                        Text = "اسناد موفقانه ارسال گردید",
                        Description = "اسناد انتخاب شده، موفقانه به مرحله تعیین شده ارسال گردید"
                    });
                }
                else
                {
                    return new JsonResult(new UIResult()
                    {
                        Data = null,
                        Status = UIStatus.Failure,
                        Text = "کوشش خلاف اصول",
                        Description = "شما اجازه ارسال این سند را به مراحل انتخاب شده ندارید. سند مذکور خارج از حدود صلاحیت این مرحله میباشد"
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }


        public async Task<IActionResult> OnPostSearch([FromBody] SearchProcessTrackQuery query)
        {
            try
            {
                query.ModuleId = Convert.ToInt16(HttpContext.Session.GetInt32("ModuleID").Value);
                var QueryResult = await Mediator.Send(query);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = QueryResult },
                    Status = UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
    }
}