using App.Application.Lookup.Models;
using App.Persistence.Context;
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
    public class GetAddressTypeList : IRequest<List<AddressTypeModel>>
    {
        public int? ID { get; set; }
    }

    public class GetAddressTypeListHandler : IRequestHandler<GetAddressTypeList, List<AddressTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetAddressTypeListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<AddressTypeModel>> Handle(GetAddressTypeList request, CancellationToken cancellationToken)
        {
            var query = Context.AddressTypes.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new AddressTypeModel
            {
                ID = e.Id,
                Name = e.Name,
                NameEn = e.NameEn
            }).ToListAsync();
        }
    }
}
