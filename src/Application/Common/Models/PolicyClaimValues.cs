using System.Collections.Generic;
using Application.Common.Enums;

namespace Application.Common.Models
{
    public class PolicyClaimValues
    {
        public string PolicyName { get; set; }
        public IList<Roles> RequiredRoles { get; set; }
        public IList<Enums.Permission> RequiredPermissions { get; set; }
    }
}
