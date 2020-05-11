using App.Application.Management.Models;
using App.Application.Management.Queries;
using App.Domain.Entity.blk;
using App.Domain.Entity.pas;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Common.Extensions;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Commands
{
    public class SaveBlackListCommand : IRequest<List<SearchBlackListModel>>
    {
        public int? Id { get; set; }
        public int BlackListProfileId { get; set; }
        public int BlackListReasonId { get; set; }
        public DateTime BlackListDate { get; set; }
        public int RequestedById { get; set; }
        public string PassportNumber { get; set; }
        public string Comments { get; set; }

    }

    public class SaveBlackListCommandHandler : IRequestHandler<SaveBlackListCommand, List<SearchBlackListModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveBlackListCommandHandler(AppDbContext context,IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchBlackListModel>> Handle(SaveBlackListCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            if (request.Id.HasValue)
            {
                var cur = Context.BlackLists.Where(e => e.Id == request.Id).Single();
                cur.BlackListReasonId = request.BlackListReasonId;
                cur.BlackListDate = request.BlackListDate;
                cur.Comments = request.Comments;
                cur.PassportNumber = request.PassportNumber;
                cur.RequestedById = request.RequestedById;
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchBlackListQuery { ID = cur.Id });
            }
            else
            {
                var cur = new BlackList
                {
                    BlackListProfileId = request.BlackListProfileId,
                    BlackListReasonId = request.BlackListReasonId,
                    RequestedById = request.RequestedById,
                    PassportNumber = request.PassportNumber,
                    BlackListDate = request.BlackListDate,
                    Comments = request.Comments,
                    StatusId = BlackListStatus.Active,
                    CreatedBy = UserID,
                    CreatedOn = DateTime.Now
                };
                

                Context.BlackLists.Add(cur);
                await Context.SaveChangesAsync();

                var prf = await Context.BlackListProfiles.Where(e => e.Id == cur.BlackListProfileId).SingleAsync();
                prf.ProfileId = GetMathcingProfile(prf);

                await Context.SaveChangesAsync();

                return await Mediator.Send(new SearchBlackListQuery { ID = cur.Id });
            }
        }



        private string GetMathcingProfile(BlackListProfile cur)
        {
            if(Context.BlackLists.Where(e => e.BlackListProfileId == cur.Id).Any())
            {
                var list = new List<Profile>();
                if (!String.IsNullOrEmpty(cur.ProfileId))
                {
                    var prfIds = cur.ProfileId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => Convert.ToInt32(e));
                    list = Context.Profiles.Where(e =>  prfIds.Contains(e.Id)).ToList();
                }
                var matches = Context.ProfileHashes.Where(e => e.HashKey == cur.HashKey).Select(e => e.Profile).Distinct().ToList();
                matches.ForEach(e => e.StatusId = ProfileStatus.BlackList);
                list.RemoveAll(e => matches.Select(f => f.Id).Contains(e.Id));
                list.ForEach(e => e.StatusId = ProfileStatus.Active);
                if (matches.Any())
                {
                    return matches.Select(e => e.Id.ToString()).Aggregate((s1, s2) => String.Concat(s1, ',', s2));
                }
            }
            return null;
        }

    }
}
