using Clean.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Persistence.Identity.Policies
{
    public class SuperAdminOnly : IAuthorizationRequirement
    {

    }
    public class SuperAdminOnlyHandler : AuthorizationHandler<SuperAdminOnly>
    {
        private readonly ICurrentUser _currentUser;

        public SuperAdminOnlyHandler(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuperAdminOnly requirement)
        {
            bool? IsSuperAdmin = await _currentUser.IsSuperAdmin();

            if (IsSuperAdmin ?? false)
                context.Succeed(requirement);
            else
                context.Fail();

        }
    }
}
