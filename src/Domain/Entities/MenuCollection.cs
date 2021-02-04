using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MenuCollection : AuditableEntity
    {
        public MenuCollection()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int Id { get; set; }
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public int Weight { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
