using App.Application.Account.Models;
using App.Application.Account.Queries;
using App.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Account.Commands
{
    public class RemoveOfficePrint : IRequest<List<SearchUserPrintOfficeModel>>
    {
        public int ID { get; set; }
    }
    public class RemoveOfficePrintHandler : IRequestHandler<RemoveOfficePrint, List<SearchUserPrintOfficeModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        public RemoveOfficePrintHandler(AppDbContext context,IMediator mediator)
        {
            Context = context;
            Mediator = mediator;
        }
        public async Task<List<SearchUserPrintOfficeModel>> Handle(RemoveOfficePrint request, CancellationToken cancellationToken)
        {
            var cur = Context.UserOfficePrinters.Where(e => e.Id == request.ID).Single();
            Context.UserOfficePrinters.Remove(cur);
            await Context.SaveChangesAsync();
            return await Mediator.Send(new GetUserPrintOfficeQuery { UserID = cur.UserId });
        }
    }
}
