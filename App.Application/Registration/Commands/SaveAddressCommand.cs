using App.Application.Registration.Models;
using App.Application.Registration.Queries;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Commands
{
    public class SaveAddressCommand : IRequest<List<SearchAddressModel>>
    {
        public int? Id { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public string City { get; set; }
        public int DistrictId { get; set; }
        public string Village { get; set; }
        public string Detail { get; set; }
        public int ProfileId { get; set; }
        public int AddressTypeId { get; set; }
    }

    public class SaveAddressCommandHandler : IRequestHandler<SaveAddressCommand, List<SearchAddressModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveAddressCommandHandler(AppDbContext context,IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchAddressModel>> Handle(SaveAddressCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var cad = new Address
            {
                ProfileId = request.ProfileId,
                AddressTypeId = request.AddressTypeId,
                CountryId = request.CountryId,
                ProvinceId = request.ProvinceId,
                DistrictId = request.DistrictId,
                City = request.City,
                Village = request.Village,
                Detail = request.Detail,
            };
            if (request.Id.HasValue)
            {
                Address cur = Context.Addresses.Where(e => e.Id == request.Id).Single();
                if(cur.CountryId != cad.CountryId || cur.ProvinceId != cad.ProvinceId || 
                    cur.DistrictId != cad.DistrictId || cur.AddressTypeId != cad.AddressTypeId)
                {
                    cad.CreatedBy = UserID;
                    cad.CreatedOn = DateTime.Now;
                    cad.StatusId = 1;
                    cur.StatusId = 0;
                    Context.Addresses.Add(cad);
                }
                else
                {
                    cur.City = cad.City;
                    cur.Village = cad.Village;
                    cur.Detail = cad.Detail;
                    cur.ModifiedBy = UserID;
                    cur.ModifiedOn = DateTime.Now;
                    cad.Id = cur.Id;
                }
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchAddressQuery { ID = cad.Id });
            }
            else
            {
                cad.StatusId = 1;
                cad.CreatedBy = UserID;
                cad.CreatedOn = DateTime.Now;
                Context.Addresses.Add(cad);
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchAddressQuery { ID = cad.Id });
            }
        }
    }
}
