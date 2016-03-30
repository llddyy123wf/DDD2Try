using System;

namespace DDD2Try.EntityFramework.Entity
{
    /// <summary>
    ///     产品和汽车的关系表
    /// </summary>
    public class Product_Car
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CarId { get; set; }
        public bool IsEffective { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}