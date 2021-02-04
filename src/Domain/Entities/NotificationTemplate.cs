using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class NotificationTemplate : AuditableEntity
    {
        public Guid Id { get; set; }
        public string SystemName { get; set; }
        public string Subject { get; set; }
        public string SystemTemplate { get; set; }
        public bool SendSystemNotification { get; set; }
        public string EmailTemplate { get; set; }
        public bool SendEmailTemplate { get; set; }
        public string SMSTemplate { get; set; }
        public bool SendSMSTemplate { get; set; }
    }
}
