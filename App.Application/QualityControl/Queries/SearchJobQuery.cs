﻿using App.Application.QualityControl.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.QualityControl.Queries
{
    public class SearchJobQuery :IRequest<List<SearchJobModel>>
    {
        public int? ID { get; set; }
        public int? PassportPrintId { get; set; }
    }

    public class SearchJobQueryHandler : IRequestHandler<SearchJobQuery, List<SearchJobModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchJobQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchJobModel>> Handle(SearchJobQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Jobs
                .AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.PassportPrintId.HasValue)
            {
                query = Context.PassportPrints.Where(e => e.Id == request.PassportPrintId)
                    .Select(e => e.PrintQueue.Application.ActiveJob);
            }

            return await query.Select(e => new SearchJobModel
            {
                Id = e.Id,
                OrganizationId = e.OrganizationId,
                OccupationId = e.OccupationId,
                ProfileId = e.ProfileId,
                Employer = e.Employer,
                EmployerAddress = e.EmployerAddress,
                PrevEmployer = e.PrevEmployer,
                PrevEmployerAddress = e.PrevEmployerAddress,
                Organization = e.Organization.Name,
                Occupation = e.Occupation.Title,
                OccupationEn = e.Occupation.TitleEn
            }).ToListAsync();
        }
    }
}