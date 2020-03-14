using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Document
{
    public class ProcessModel : BasePage
    {
        private readonly IConfiguration _configuration;
        [BindProperty]
        public string LogicID { get; set; }
        private ICurrentUser _currentUser;

        //public ProcessModel(IConfiguration configuration, ICurrentUser currentUser)
        //{
        //    _configuration = configuration;
        //    _currentUser = currentUser;
        //}

        //public async Task<IActionResult> OnGetAsync([FromQuery] int? id = null, [FromQuery] int? recordid = null)
        //{

        //    try
        //    {
        //        LogicID = id.ToString();// Convert.ToString(id);
        //        //PreProcessCheckupRes res;
        //        //if (id.HasValue && id != default(int))
        //        //    res = await Mediator.Send(new PreProcessCehckupQuery() { PreProcess = id , ApplicationID = recordid });

        //        ListOfProcessesConnection = new List<SelectListItem>();
        //        int ScreenID;

        //        ScreenID = int.TryParse(HttpContext.Request.Query["p"], out ScreenID) ? ScreenID : Convert.ToInt32(EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]));


        //        List<SearchedProcessConnection> ProcessConnections = await Mediator.Send(new GetProcessConnection() { });

        //        List<SearchedProcess> Processes = await Mediator.Send(new GetProcess() { ScreenId = ScreenID });


        //        ProcessConnections = ProcessConnections.Where(c => c.ScreenId == ScreenID).ToList();

        //        //1.
        //        // check if request record is for change to hier application then load only change to heir process connections
        //        if (Context.Application.Where(e => e.ProfileId ==
        //        (Context.Application.Where(f => f.Id == recordid).SingleOrDefault().ProfileId)
        //        && e.Status == (int)StatusEnum.ChangeToHeirApplication).Any())
        //        {
        //            ProcessConnections = ProcessConnections.Where(e => e.ConnectionId != (int)SystemProcesses.Application && e.ConnectionId != (int)SystemProcesses.PDApplication).ToList();
        //        }
        //        //2.
        //        // check if request record is for PD application then load only PD application process connection
        //        else if (Context.Profile.Where(e => e.Id == Context.Application.Where(d => d.Id == recordid).FirstOrDefault().ProfileId && e.CensusId != null).Any())
        //        {
        //            ProcessConnections = ProcessConnections.Where(e => e.ConnectionId != (int)SystemProcesses.Application && e.ConnectionId != (int)SystemProcesses.ChangeToHeir).ToList();

        //        }
        //        //3.
        //        //load only organizations process connections
        //        else
        //        {
        //            ProcessConnections = ProcessConnections.Where(e => e.ConnectionId != (int)SystemProcesses.ChangeToHeir && e.ConnectionId != (int)SystemProcesses.PDApplication).ToList();
        //        }
        //        //find process id
        //        //remove from process connection others

        //        if (ProcessConnections.Any())
        //        {
        //            foreach (SearchedProcessConnection PC in ProcessConnections)
        //            {

        //                ListOfProcessesConnection.Add(new SelectListItem(PC.ConnectionText, PC.ConnectionId.ToString()));

        //            }
        //            if (Processes.Any())
        //            {
        //                HttpContext.Session.SetInt32("ModuleID", Processes.FirstOrDefault().ModuleId);
        //                HttpContext.Session.SetInt32("ProcessID", Processes.FirstOrDefault().Id);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(CustomMessages.FabricateException(ex));
        //    }

        //    return Page();

        //}


        //public async Task<IActionResult> OnPostSave([FromBody] SaveProcessTracksCommand command)
        //{
        //    try
        //    {
        //        List<SearchedProcessTracks> QueryResult = await Mediator.Send(new SearchProcessTrackQuery() { RecordId = command.RecordId/*, ModuleId =Convert.ToInt16(HttpContext.Session.GetInt32("ModuleID").Value)*/ });
        //        SearchedProcessTracks CurrentProcess = QueryResult.FirstOrDefault();

        //        if (command.Id == CurrentProcess.Id && CurrentProcess.ProcessId == HttpContext.Session.GetInt32("ProcessID"))
        //        {
        //            command.ModuleId = CurrentProcess.ModuleId;
        //            command.UserId = await _currentUser.GetUserId();
        //            command.CategoryId = await _currentUser.GetUserCaegory();
        //            QueryResult = await Mediator.Send(command);
        //            return new JsonResult(new UIResult()
        //            {
        //                Data = new { list = QueryResult },
        //                Status = UIStatus.Success,
        //                Text = "اسناد موفقانه ارسال گردید",
        //                Description = "اسناد انتخاب شده، موفقانه به مرحله تعیین شده ارسال گردید"
        //            });
        //        }
        //        else
        //        {
        //            return new JsonResult(new UIResult()
        //            {
        //                Data = null,
        //                Status = UIStatus.Failure,
        //                Text = "کوشش خلاف اصول",
        //                Description = "شما اجازه ارسال این سند را به مراحل انتخاب شده ندارید. سند مذکور خارج از حدود صلاحیت این مرحله میباشد"
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(CustomMessages.FabricateException(ex));
        //    }
        //}


        //public async Task<IActionResult> OnPostSearch([FromBody] SearchProcessTrackQuery query)
        //{
        //    try
        //    {
        //        query.ModuleId = Convert.ToInt16(HttpContext.Session.GetInt32("ModuleID").Value);

        //        List<SearchedProcessTracks> QueryResult = new List<SearchedProcessTracks>();
        //        QueryResult = await Mediator.Send(query);
        //        return new JsonResult(new UIResult()
        //        {
        //            Data = new { list = QueryResult },
        //            Status = UIStatus.Success,
        //            Text = string.Empty,
        //            Description = string.Empty
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(CustomMessages.FabricateException(ex));
        //    }
        //}
    }
}