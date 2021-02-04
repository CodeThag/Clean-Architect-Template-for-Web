using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrganisationType : AuditableEntity
    {
        public OrganisationType()
        {
            Organisations = new HashSet<Organisation>();
            ApplicationTypes = new HashSet<ApplicationType>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<Organisation> Organisations { get; set; }
        public virtual ICollection<ApplicationType> ApplicationTypes { get; set; }
    }
}