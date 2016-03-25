using System.ComponentModel;

namespace PhotoMaps.Domain
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        [Description("未激活")]
        UnActivated = 0x01,

        [Description("激活")]
        Activated = 0x02,

        [Description("锁定")]
        Lcoked = 0x03,

        [Description("作废")]
        Cancelled = 0x04,
    }
}
