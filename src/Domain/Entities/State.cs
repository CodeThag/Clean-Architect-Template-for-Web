using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class State : AuditableEntity
    {
        public State()
        {
            Cities = new HashSet<City>();
        }
        public int Id { get; set; }
        public string Iso2 { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string FipsCode { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}