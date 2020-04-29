using App.Application.Registration.Models;
using App.Application.Registration.Queries;
using App.Persistence.Context;
using Clean.Common;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Common.Storage;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Commands
{
    public class SaveAttachmentCommand:IRequest<List<AttachmentModel>>
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public int AttachmentTypeId { get; set; }
        public int ProfileId { get; set; }
        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ContentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Description { get; set; }
    }
    public class SaveAttachmentCommandHandler : IRequestHandler<SaveAttachmentCommand, List<AttachmentModel>>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        private IMediator Mediator { get; set; }
        public SaveAttachmentCommandHandler(AppDbContext dbContext,ICurrentUser currentUser,IMediator mediator)
        {
            Context = dbContext;
            CurrentUser = currentUser;
            Mediator = mediator;
        }
        public async Task<List<AttachmentModel>> Handle(SaveAttachmentCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var apps = await Context.PassportApplications.Where(e => e.ProfileId == request.ProfileId).ToListAsync();
            if (apps.Any())
            {
                var capp = apps.OrderByDescending(e => e.Id).First();
                if (capp.CurProcessId != SystemProcess.Registration && capp.CurProcessId != SystemProcess.Close)
                {
                    throw new BusinessRulesException("این درخواست قابل تغییر نمی باشد!");
                }
            }
            var attach = request.Id.HasValue ? Context.Attachments.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Attachments();

            attach.AttachmentTypeId = request.AttachmentTypeId;
            attach.ProfileId = request.ProfileId;
            attach.DocumentDate = request.DocumentDate;
            attach.DocumentNumber = request.DocumentNumber;
            attach.Description = request.Description;
            attach.Name = request.Name;
            attach.Path = request.Path;
            FileStorage fs = new FileStorage();
            attach.ContentType = fs.GetContentType(AppConfig.AttachmentsPath + request.Path);

            ///Encryption Setting 
            attach.IsEncrypted = false;
            attach.EncryptionKey = null;

            attach.CreatedBy = UserID;
            attach.CreatedOn = DateTime.Now;

            if (!request.Id.HasValue)
            {
                Context.Attachments.Add(attach);
            }

            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchAttachmentQuery { ID = attach.Id });

        }
    }
}
