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
    public class GetColorsList : IRequest<List<ColorModel>>
    {
        public int? ID { get; set; }
        public string ColorType { get; set; }
    }

    public class GetColorsListHandler : IRequestHandler<GetColorsList, List<ColorModel>>
    {
        private AppDbContext Context { get; set; }
        public GetColorsListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<ColorModel>> Handle(GetColorsList request, CancellationToken cancellationToken)
        {
            var query = Context.Colors.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (!String.IsNullOrEmpty(request.ColorType))
            {
                query = query.Where(e => e.ColorType == request.ColorType);
            }

            return await query.Select(e => new ColorModel
            {
                ID = e.Id,
                Name = e.Name,
                NameEn = e.NameEn
            }).ToListAsync();
        }
    }
}
