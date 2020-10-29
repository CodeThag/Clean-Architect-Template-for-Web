using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum Roles
    {
        [StringValue("Cube Admin")]
        CubeAdmin,
        [StringValue("Cube Client Admin")]
        CubeClientAdmin,
        [StringValue("Cube Client User")]
        CubeClientUser,
        [StringValue("Cube Applicant")]
        CubeApplicant

    }
}
