using System.Collections.Generic;
using Application.Common.Interfaces;

namespace Application.Common.Models
{
    public class RoleQueryListResult : PageInfoModel, IPayLoadObject
    {
        public RoleQueryListResult()
        {
            Roles = new List<Role>();
        }

        public IEnumerable<Role> Roles { get; set; }
    }
}
