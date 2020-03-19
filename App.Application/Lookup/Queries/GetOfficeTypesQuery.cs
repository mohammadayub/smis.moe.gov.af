using App.Application.Lookup.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetOfficeTypesQuery: IRequest<List<OfficeTypeModel>>
    {

    }

    public class GetOfficeTypesQueryHandler : IRequestHandler<GetOfficeTypesQuery, List<OfficeTypeModel>>
    {
        public async Task<List<OfficeTypeModel>> Handle(GetOfficeTypesQuery request, CancellationToken cancellationToken)
        {
            var list = new List<OfficeTypeModel>
            {
                new OfficeTypeModel { ID = 1, Title = "دفترمرکزی" },
                new OfficeTypeModel { ID = 2, Title = "سفارت خانه" },
                new OfficeTypeModel { ID = 3, Title = "کنسولگری" }
            };

            return list;
        }
    }
}
