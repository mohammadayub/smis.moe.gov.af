using App.Application.Management.Models;
using App.Application.Management.Queries;
using App.Domain.Entity.blk;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Commands
{
    public class SaveWhiteListCommand : IRequest<List<SearchWhiteListModel>>
    {
        public int? Id { get; set; }
        public int BlackListId { get; set; }
        public int RequestedById { get; set; }
        public DateTime WhiteListDate { get; set; }
        public string Comments { get; set; }

    }

    public class SaveWhiteListCommandHandler : IRequestHandler<SaveWhiteListCommand, List<SearchWhiteListModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveWhiteListCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchWhiteListModel>> Handle(SaveWhiteListCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var cur = request.Id.HasValue ? Context.WhiteLists.Where(e => e.Id == request.Id).Single() : new WhiteList();

            cur.BlackListId = request.BlackListId;
            cur.WhiteListDate = request.WhiteListDate;
            cur.Comments = request.Comments;
            cur.RequestedById = request.RequestedById;

            if (request.Id.HasValue)
            {
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;

            }
            else
            {
                cur.CreatedBy = UserID;
                cur.CreatedOn = DateTime.Now;
                Context.WhiteLists.Add(cur);
                var blk = Context.BlackLists.Where(e => e.Id == cur.BlackListId).Select(e => new { e.BlackListProfile, BlackList = e }).Single();
                blk.BlackList.StatusId = BlackListStatus.Resolved;
                blk.BlackListProfile.StatusId = BlackListStatus.Resolved;
                if(!String.IsNullOrEmpty(blk.BlackListProfile.ProfileId))
                {
                    var profileIds = blk.BlackListProfile.ProfileId
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(e => Convert.ToInt32(e)).ToList();

                    var profiles = Context.Profiles.Where(e => profileIds.Contains(e.Id)).ToList();
                    profiles.ForEach(e => e.StatusId = ProfileStatus.Active);
                }
            }
            await Context.SaveChangesAsync();

            return await Mediator.Send(new SearchWhiteListQuery { ID = cur.Id });


        }
    }
}
