using System;

namespace DDD2Try.EntityFramework.Entity
{
    /// <summary>
    ///     产品表
    /// </summary>
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductNo { get; set; }
        public string SerialNumber { get; set; }
        public string VipCardNumber { get; set; }
        public string VipCardNumberPassword { get; set; }
        public RegisterStatus RegisterStatus { get; set; }
        public DateTime RegisterTime { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid ModifyUserId { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}