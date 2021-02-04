using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class SystemNotification : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid Recipient { get; set; }
        public Guid Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Read { get; set; }
    }
}
