using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Printing.Queries
{
    public class SearchPrintingOfficeQuery: IRequest<PrintingOfficeModel>
    {
        public int UserID { get; set; }
    }

    public class SearchPrintingOfficeQueryHandler : IRequestHandler<SearchPrintingOfficeQuery, PrintingOfficeModel>
    {
        private AppDbContext Context { get; }
        private AppIdentityDbContext IdentityDbContext { get; }
        public SearchPrintingOfficeQueryHandler(AppDbContext context,AppIdentityDbContext identityDbContext)
        {
            Context = context;
            IdentityDbContext = identityDbContext;
        }
        public async Task<PrintingOfficeModel> Handle(SearchPrintingOfficeQuery request, CancellationToken cancellationToken)
        {
            return await IdentityDbContext.Users.Where(e => e.Id == request.UserID).Select(e => e.Office)
                .Select(e => new PrintingOfficeModel
                {
                    ID = e.Id,
                     OfficeName = e.Title,
                     OfficeNameEn = e.TitleEn
                })
                .SingleAsync();
        }
    }
}
