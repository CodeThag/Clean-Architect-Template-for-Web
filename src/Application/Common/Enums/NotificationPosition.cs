using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum NotificationPosition
    {
        [StringValue("toast-top-right")]
        TopRight,
        [StringValue("toast-top-left")]
        TopLeft,
        [StringValue("toast-bottom-right")]
        BottomRight,
        [StringValue("toast-bottom-left")]
        BottomLeft,
        [StringValue("toast-top-full-width")]
        TopFullWidth,
        [StringValue("toast-bottom-full-width")]
        BottomFullWidth,
        [StringValue("toast-top-center")]
        TopCenter,
        [StringValue("toast-bottom-center")]
        BottomCenter
    }
}