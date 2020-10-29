using System.Collections.Generic;
using Application.Common.Enums;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUserId();
        int GetUserOrganisationId();
        string GetUserName(string userId);
        bool UserIsInRole(string role);
        IList<string> GetCurrentUserRoles();
        bool UserHasRole(Roles role);

    }
}
