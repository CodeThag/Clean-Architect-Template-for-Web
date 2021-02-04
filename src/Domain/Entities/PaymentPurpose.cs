using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class PaymentPurpose : AuditableEntity
    {
        public PaymentPurpose()
        {
            PaymentPurposeFixedLineItems = new HashSet<PaymentPurposeFixedLineItem>();
        }

        public Guid Id { get; set; }
        public Guid PaymentTypeId { get; set; }
        public Guid ApplicationTypeId { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual ApplicationType ApplicationType { get; set; }
        public virtual ICollection<PaymentPurposeFixedLineItem> PaymentPurposeFixedLineItems { get; set; }
    }
}
