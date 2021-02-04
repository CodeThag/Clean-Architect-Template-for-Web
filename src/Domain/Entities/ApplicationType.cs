using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class ApplicationType : AuditableEntity
    {
        public ApplicationType()
        {
            OrganisationTypes = new HashSet<OrganisationType>();
            PaymentPurposes = new HashSet<PaymentPurpose>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Guidelines { get; set; }
        public bool IsActive { get; set; }
        public bool HasWorkflow { get; set; }
        public string WorkflowCode { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<OrganisationType> OrganisationTypes { get; set; }
        public virtual ICollection<PaymentPurpose> PaymentPurposes { get; set; }
    }
}