using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class AnsweredAttributes
    {
        public int AttributeId { get; set; }
        public int ValueId { get; set; }
    }

    public class AttributeValueModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<ValueModel> Values { get; set; }
        public int SelectedValue { get; set; }
    }

    public class AnsweredAttributeValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ValueId { get; set; }
        public string Value { get; set; }
        public bool Assumed { get; set; }
    }

    public class ValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
