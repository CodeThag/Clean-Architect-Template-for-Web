using System.Collections.Generic;

namespace Application.Common.Configuration
{
    public class AdminConfiguration
    {
        public string AuthenticationBaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public List<string> Scope { get; set; }
        public string ProfileScope { get; set; }
        public string OpenIdScope { get; set; }
        public string SsoApiScope { get; set; }
        public string NotificationApiScope { get; set; }
        public string SubscriptionApiScope { get; set; }
        public string SubscriptionClaim { get; set; }
        public string ResponseType { get; set; }
        public string ResponseMode { get; set; }
        public string DocumentRepoUrl { get; set; }
        public string ChamberRegistrationWorkflowScheme { get; set; }
        public string TraderRegistrationWorkflowScheme { get; set; }
        public string AgentRegistrationWorkflowScheme { get; set; }


    }
}
