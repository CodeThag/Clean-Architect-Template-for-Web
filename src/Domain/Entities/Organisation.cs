using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Organisation : AuditableEntity
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string RCNumber { get; set; }
        public string BVN { get; set; }
        public Guid OrganisationTypeId { get; set; }
        public virtual OrganisationType OrganisationType { get; set; }
    }
}
