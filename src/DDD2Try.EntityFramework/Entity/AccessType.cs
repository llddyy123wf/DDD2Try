using System;
using System.ComponentModel;

namespace DDD2Try.Domain
{
    /// <summary>
    /// 访问类型
    /// </summary>
    [Flags]
    public enum AccessType
    {
        [Description("公开")]
        Public = 0x01,

        [Description("保护")]
        Protected = 0x02,

        [Description("朋友")]
        Friend = 0x04,

        [Description("私有")]
        Private = 0x08,
    }
}
