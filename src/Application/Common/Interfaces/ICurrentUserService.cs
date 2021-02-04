using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Enums;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUserId();
        string GetUserName();
        bool UserIsInRole(string role);
        IList<string> GetCurrentUserRoles();
        bool UserHasRole(Roles role);
    }
}
