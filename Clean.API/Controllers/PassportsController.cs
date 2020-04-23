using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Passport.Queries;
using App.Application.Printing.Commands;
using App.Application.Printing.Models;
using App.Application.Printing.Queries;
using Clean.API.Models.Passport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PassportsController : ControllerBase
    {
        private IMediator Mediator { get;  }
        public PassportsController(IMediator mediator)
        {
            Mediator = mediator;
        }
        [HttpPost("list")]
        public async Task<ActionResult<List<PrintPassportInformation>>> GetList(PassportListRequest request)
        {
            return await Mediator.Send(new GetPassportsForPrintList { PassportTypeID = request.PassportTypeID,PassportDurationID = request.PassportDurationID });
        }

        [HttpPost("types")]
        public async Task<ActionResult> GetPassportTypes()
        {
            var ptypes = await Mediator.Send(new GetPassportTypeAndDurationQuery());
            return new JsonResult( ptypes);
        }

        [HttpPost("fullinfo")]
        public async Task<ActionResult<PassportPrintFullInformation>> GetList(GetPassportPrintFullInformationQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("process")]
        public async Task<ActionResult<PassportPrintProcessModel>> ProcessPassport(ProcessPrintedPassportCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}