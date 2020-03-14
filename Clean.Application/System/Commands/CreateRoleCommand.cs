using Clean.Application.System.Models;
using Clean.Application.System.Queries;
using Clean.Common.Exceptions;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.System.Commands
{
    public class CreateRoleCommand : IRequest<List<SearchedRoleModel>>
    {
        [MinLength(4, ErrorMessage = "نام رول حداقل باید دارای چهار حرف باشد")]
        [MaxLength(32, ErrorMessage = "نام رول حد اکثر میتواند دارای سی و دو حرف باشد")]
        [Required]
        public string RoleName { get; set; }
        public int? Id { get; set; }

    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, List<SearchedRoleModel>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMediator _mediator;

        public CreateRoleCommandHandler(UserManager<AppUser> user, RoleManager<AppRole> role, IMediator mediator)
        {
            _userManager = user;
            _roleManager = role;
            _mediator = mediator;
        }

        public async Task<List<SearchedRoleModel>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            List<SearchedRoleModel> fresult = new List<SearchedRoleModel>();

            if (!string.IsNullOrEmpty(request.RoleName))
            {
                IdentityResult result;
                if (request.Id.HasValue)
                {
                    result = await _roleManager.UpdateAsync(await _roleManager.FindByIdAsync(request.Id.ToString()));
                }
                else
                {
                    result = await _roleManager.CreateAsync(new AppRole() { Name = request.RoleName });
                }

                if (result.Succeeded)
                {

                    fresult = await _mediator.Send(new GetRoleQuery() { RoleName = request.RoleName });
                }
                else
                {
                    StringBuilder ErrorBuilder = new StringBuilder();
                    foreach (IdentityError error in result.Errors)
                        ErrorBuilder.Append(error.Description).Append("\n");

                    throw new BusinessRulesException(ErrorBuilder.ToString());
                }

            }
            else
            {
                throw new BusinessRulesException("نام نقش خالی بوده نمیتواند");
            }
            return fresult;
        }
    }
}
