using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum NotificationType
    {
        [StringValue("success")]
        Success,
        [StringValue("info")]
        Info,
        [StringValue("warning")]
        Warning,
        [StringValue("danger")]
        Error
    }
}