using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class PaymentPurposeFixedLineItem : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PaymentPurposeId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsTaxable { get; set; }
        public decimal Tax { get; set; }
        public int Weight { get; set; }
        public bool IsSelectable { get; set; }
        public virtual PaymentPurpose PaymentPurpose { get; set; }
    }
}
