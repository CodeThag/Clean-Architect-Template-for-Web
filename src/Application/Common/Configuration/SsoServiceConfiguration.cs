namespace Application.Common.Configuration
{
    public class SsoServiceConfiguration
    {
        public string SsoServiceBaseUrl { get; set; }
        public string AddUserPostUrl { get; set; }
        public string AddUserRolePostUrl { get; set; }
        public string AddClaimToUserPostUrl { get; set; }
        public string GetUsersUrl { get; set; }
        public string GetRolesUrl { get; set; }
        public string AddClaimListToUserPostUrl { get; set; }
        public string ChangePassword { get; set; }
        public string GetUsersByClaimsPostUrl { get; set; }
        public string GetUsersByOrgPostUrl { get; set; }
        public string GetUserClaimByUserIdPostUrl { get; set; }
    }
}
