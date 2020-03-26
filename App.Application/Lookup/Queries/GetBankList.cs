using App.Application.Lookup.Models;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetBankList: IRequest<List<BankModel>>
    {
        public int? ID { get; set; }
        public bool LimitByUser { get; set; } = true;
    }

    public class GetBankListHandler : IRequestHandler<GetBankList, List<BankModel>>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        public GetBankListHandler(AppDbContext context,ICurrentUser currentUser)
        {
            Context = context;
            CurrentUser = currentUser;
        }
        public async Task<List<BankModel>> Handle(GetBankList request, CancellationToken cancellationToken)
        {
            var query = Context.Banks.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            if (request.LimitByUser)
            {
                var OfficeID = await CurrentUser.GetOfficeID();
                var CountryID = Context.Offices.Where(e => e.Id == OfficeID).Select(e => e.CountryId).Single();
                query = query.Where(e => e.CountryId == CountryID);
            }

            return await query.Select(e => new BankModel
            {
                ID = e.Id,
                Title = e.Title,
                TitleEn = e.TitleEn,
                CountryID = e.CountryId
            }).ToListAsync();
        }
    }
}
