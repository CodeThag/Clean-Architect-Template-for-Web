using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class City : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
    }
}