using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Views { get; set; } = 0;
        public string Content { get; set; }
        public string Excerpt { get; set; }
        public string CoverImagePath { get; set; }
        public bool Public { get; set; }
    }
}