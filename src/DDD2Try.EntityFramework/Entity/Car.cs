using System;

namespace DDD2Try.EntityFramework.Entity
{
    /// <summary>
    /// 车辆表
    /// </summary>
	public class Car
	{
		public Guid Id { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 车系ID
        /// </summary>
        public Guid CarsId { get; set; }

        /// <summary>
        /// 车型ID
        /// </summary>
        public Guid CarModelId { get; set; }

        /// <summary>
        /// 年款ID
        /// </summary>
        public Guid CarVersionId { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlateNumber { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string Vin { get; set; }

		public string Description { get; set; }

        public string PictureUrl { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid ModifyUserId { get; set; }

        public DateTime ModifyTime { get; set; }
       
	}
}