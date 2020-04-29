using App.Application.Account.Models;
using Clean.Persistence.Identity;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Account.Commands
{
    public class ChangeAccountPasswordCommand:IRequest<ChangeAccountPasswordModel>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand, ChangeAccountPasswordModel>
    {
        private UserManager<AppUser> UserManger { get; }
        private IHttpContextAccessor Context { get; }
        public ChangeAccountPasswordCommandHandler(UserManager<AppUser> usermanager, IHttpContextAccessor httpContextAccessor)
        {
            UserManger = usermanager;
            Context = httpContextAccessor;
        }
        public async Task<ChangeAccountPasswordModel> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            ChangeAccountPasswordModel response = new ChangeAccountPasswordModel {Success = false };
            if (Context.HttpContext.User.Identity.IsAuthenticated)
            {
                var User = await UserManger.GetUserAsync(Context.HttpContext.User);
                var result = await UserManger.ChangePasswordAsync(User, request.OldPassword, request.NewPassword);
                if (result.Succeeded)
                {
                    response.Success = true;
                }
                else
                {
                    foreach(var err in result.Errors)
                    {
                        response.Message += err.Description + "\\n";
                    }
                }
            }
            else
            {
                response.Message = "کاربر داخل سیستم نشده است!";
            }
            return response;
        }
    }

}
