using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Setup.ApplicationTypes.Queries.GetApplicationType
{
    public class ApplicationTypeDto : IMapFrom<ApplicationType>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool HasWorkflow { get; set; }
        public string WorkflowCode { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime LastModified { get; set; }
    }
}
