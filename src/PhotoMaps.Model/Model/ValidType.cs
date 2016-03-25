﻿using System.ComponentModel;

namespace PhotoMaps.Domain
{
    /// <summary>
    /// 验证类型
    /// </summary>
    public enum ValidType
    {
        [Description("精确")]
        Exact = 1,

        [Description("模糊")]
        Fuzzy = 2
    }
}
