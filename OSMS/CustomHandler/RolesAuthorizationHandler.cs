using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using OSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS.CustomHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly OSMSContext _dbContext;
        public RolesAuthorizationHandler(OSMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles == null ||
                requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userID = claims.FirstOrDefault(c => c.Type == "UserID").Value;
                var roles = requirement.AllowedRoles;

                if(roles.Contains("Instructor"))
                {
                    validRole = _dbContext.Instructors.Where(i => roles.Contains(i.Role.RoleName) && i.InstructorID == userID).Any();
                }
                else if(roles.Contains("Student"))
                {
                    validRole = _dbContext.Students.Where(s => roles.Contains(s.Role.RoleName) && s.StudentID == userID).Any();
                }
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
