using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum Roles
    {
        [StringValue("Administrator")]
        Administrator,
        [StringValue("Organisation Administrator")]
        OrganisationAdministrator,
        [StringValue("Organisation User")]
        OrganisationUser
    }
}
