using Application.Common.Enums;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public IList<string> GetCurrentUserRoles()
        {
            throw new NotImplementedException();
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetUserName(string userId)
        {
            var username = _httpContext.HttpContext?.User?.Identity.Name;
            return username;
        }

        public int GetUserOrganisationId()
        {
            throw new NotImplementedException();
        }

        public bool UserHasRole(Roles role)
        {
            throw new NotImplementedException();
        }

        public bool UserIsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
