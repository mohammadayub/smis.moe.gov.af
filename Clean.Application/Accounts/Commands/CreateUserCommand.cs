using Clean.Application.Accounts.Models;
using Clean.Application.Accounts.Queries;
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
    public class CreateUserCommand : IRequest<List<SearchedUsersModel>>
    {

        public int? ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int OrganizationID { get; set; }
        public bool PasswordChanged { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public int OfficeID { get; set; }
        public bool Active { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, List<SearchedUsersModel>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }
        public async Task<List<SearchedUsersModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            List<SearchedUsersModel> fresult = new List<SearchedUsersModel>();

            string GeneratedPassword = CredentialHelper.GenerateRandomPassowrd(CredentialHelper.SystemPasswordPolicy);

            bool IsUpdate = request.ID.HasValue ? true : false;


            AppUser user = IsUpdate ? await _userManager.FindByIdAsync(request.ID.ToString()) : new AppUser();

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.OrganizationID = request.OrganizationID;
            user.FirstName = request.FirstName;
            user.FatherName = request.FatherName;
            user.LastName = request.LastName;
            user.PasswordChanged = IsUpdate ? user.PasswordChanged : request.PasswordChanged;
            user.Disabled = !request.Active;
            user.OfficeID = request.OfficeID;
            user.PhoneNumber = request.Phone;

            IdentityResult result = IsUpdate ? await _userManager.UpdateAsync(user) : await _userManager.CreateAsync(user, GeneratedPassword);

            if (result.Succeeded)
            {
                fresult = await _mediator.Send(new GetUsersQuery() { Id = user.Id, GeneratedPassword = IsUpdate ? null : GeneratedPassword });
            }
            else
            {
                StringBuilder ErrorBuilder = new StringBuilder();

                foreach (IdentityError error in result.Errors)
                    ErrorBuilder.Append(error.Description).Append("\n");

                throw new BusinessRulesException(ErrorBuilder.ToString());
            }
            return fresult;
        }
    }
}
