using System;

namespace DDD2Try.Domain.Common.Model
{
    /// <summary>
    /// 验证信息
    /// 包括随机码, 验证码
    /// </summary>
    public class Validation
    {
        public int Id { get; set; }
        public string ResourceId { get; set; }

        public string ValidCode { get; set; }

        public DateTime ExpiredTime { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
