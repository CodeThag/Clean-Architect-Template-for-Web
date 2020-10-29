
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Configuration
{
    public class WorkFlowServiceConfiguration
    {
        public string WorkFlowServiceBaseUrl { get; set; }
        public string CreateInstancePostUrl { get; set; }
        public string GetInstanceInfoPostUrl { get; set; }
        public string GetAvailableCommandsPostUrl { get; set; }
        public string ExecuteCommandPostUrl { get; set; }
        public string GetAvailableStateToSetPostUrl { get; set; }
        public string SetStatePostUrl { get; set; }

    }
}
