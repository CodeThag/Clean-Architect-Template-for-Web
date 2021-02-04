using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("WorkflowScheme")]
    public class WorkflowScheme
    {
        [Key]
        [StringLength(256)]
        public string Code { get; set; }

        [Required]
        public string Scheme { get; set; }
        public bool CanBeInlined { get; set; }
        public string InlinedSchemes { get; set; }
        public string Tags { get; set; }
    }
}