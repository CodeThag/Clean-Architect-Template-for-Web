using System;
using Application.Common.Configuration;
using Application.Common.Enums;

namespace Application.Common.Helper
{
    public static class InternalProcessConfigHelper
    {
        public static string GetWorkflowScheme(this Process process, AdminConfiguration adminConfiguration)
        {
            return process switch
            {
                Process.ChamberSubscriptionProcess => adminConfiguration.ChamberRegistrationWorkflowScheme,
                Process.TraderSubscriptionProcess => adminConfiguration.TraderRegistrationWorkflowScheme,
                Process.AgentSubscriptionProcess => adminConfiguration.AgentRegistrationWorkflowScheme,
                _ => throw new ArgumentOutOfRangeException(nameof(process), process, null)
            };
        }
    }
}
