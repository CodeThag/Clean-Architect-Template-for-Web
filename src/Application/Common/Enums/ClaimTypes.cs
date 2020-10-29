using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum ClaimTypes
    {
        [StringValue("Role")]
        CubeRoles,
        [StringValue("Permissions")]
        Permissions,
        [StringValue("Organisation_Id")]
        SubscriptionClaim,
        [StringValue("middle_name")]
        MiddleName,
        [StringValue("given_name")]
        FirstName,
        [StringValue("family_name")]
        LastName,
        [StringValue("phone_number")]
        PhoneNumber
    }
}
