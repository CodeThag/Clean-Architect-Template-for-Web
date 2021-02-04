using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MenuItem : AuditableEntity
    {
        public MenuItem()
        {
            Children = new HashSet<MenuItem>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Area { get; set; }
        public string PageLink { get; set; }
        public bool IsCollapsible { get; set; }
        public string Label { get; set; }
        public int Weight { get; set; }
        public int MenuCollectionId { get; set; }
        public string Permission { get; set; }
        public string Icon { get; set; }
        public virtual MenuItem Parent { get; set; }
        public virtual MenuCollection MenuCollection { get; set; }
        public virtual ICollection<MenuItem> Children { get; set; }
    }
}
