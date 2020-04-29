using Clean.Common.Exceptions;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Accounts.Commands
{
    public class ChangePasswordCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
        public bool ResetOperation { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {

        private readonly UserManager<AppUser> _userManager;
        public ChangePasswordCommandHandler(UserManager<AppUser> usermanager)
        {
            _userManager = usermanager;
        }
        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            string fresult = string.Empty;

            AppUser CurrentUser = await _userManager.FindByNameAsync(request.UserName.Trim());

            if (CurrentUser != null)
            {
                if (!request.ResetOperation)
                {
                    if (request.NewPassword != request.NewPasswordConfirmation)
                        throw new IdentityException("رمز جدید با تائیدی آن مطابقت ندارد");

                    IdentityResult PaswordChangeResult = await _userManager.ChangePasswordAsync(CurrentUser, request.CurrentPassword, request.NewPassword);

                    if (PaswordChangeResult.Succeeded)
                    {
                        CurrentUser.PasswordChanged = true;
                        await _userManager.UpdateAsync(CurrentUser);
                        fresult = "کاربر عزیز رمز عبور شما موفقانه تغییر یافت";
                    }

                    else
                    {
                        foreach (var error in PaswordChangeResult.Errors)
                        {

                            fresult += "\n " + error.Description;
                        }
                        throw new IdentityException(fresult);
                    }
                }
                else
                {
                    // REMAINED : Reset Password
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(CurrentUser);
                    string GeneratedPassword = CredentialHelper.GenerateRandomPassowrd(CredentialHelper.SystemPasswordPolicy);
                    IdentityResult PasswordResetResult = await _userManager.ResetPasswordAsync(CurrentUser, resetToken, GeneratedPassword);
                    if (PasswordResetResult.Succeeded)
                    {
                        CurrentUser.PasswordChanged = false;
                        await _userManager.UpdateAsync(CurrentUser);
                        fresult = GeneratedPassword;
                    }

                }
            }
            else
            {
                throw new IdentityException("کابر مورد نظر دریافت نشد");
            }

            return fresult;
        }
    }
}
