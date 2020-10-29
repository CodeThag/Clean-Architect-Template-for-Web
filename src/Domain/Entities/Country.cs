using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Country : AuditableEntity
    {
        public Country()
        {
            States = new HashSet<State>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string Iso2 { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string Native { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public int Flag { get; set; }
        public string WikiDataId { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
