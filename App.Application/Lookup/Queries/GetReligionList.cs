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


        public class GetReligionList : IRequest<List<ReligionModel>>
        {
            public int? ID { get; set; }
            public int? ParentID { get; set; }
        }

        public class GetReligionListHandler : IRequestHandler<GetReligionList, List<ReligionModel>>
        {
            private readonly AppDbContext Context;
            public GetReligionListHandler(AppDbContext context)
            {
                Context = context;
            }
            public async Task<List<ReligionModel>> Handle(GetReligionList request, CancellationToken cancellationToken)
            {

                var query = Context.Religions.AsQueryable();
                if (request.ID.HasValue)
                {
                    query = query.Where(e => e.Id == request.ID.Value).AsQueryable();
                }
                else if (request.ParentID.HasValue)
                {
                    query = query.Where(e => e.ParentId == request.ParentID).AsQueryable();
                }
                return await query.Select(e => new ReligionModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ParentId = e.ParentId
                }).ToListAsync();
            }
        }
    }

