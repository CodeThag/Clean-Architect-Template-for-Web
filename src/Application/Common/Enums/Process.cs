using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum Process
    {
        [StringDescription("CUBE Chamber Registration")]
        [StringValue("CCR")]
        ChamberSubscriptionProcess,
        [StringDescription("CUBE Trader Registration")]
        [StringValue("CTR")]
        TraderSubscriptionProcess,
        [StringDescription("CUBE Agent Registration")]
        [StringValue("CAR")]
        AgentSubscriptionProcess

    }
}
