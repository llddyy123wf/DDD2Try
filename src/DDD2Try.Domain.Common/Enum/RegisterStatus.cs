using System.ComponentModel;

namespace DDD2Try.Domain.Common.Enum
{
    public enum RegisterStatus
    {
        [Description("未注册")]
        UnRegistered = 0x00,

        [Description("已注册")]
        Activated = 0x01,

        [Description("已锁定")]
        Lcoked = 0x02,

        [Description("已注销")]
        Cancelled = 0x04,
    }
}
