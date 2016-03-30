using System;

namespace DDD2Try.EntityFramework.Entity
{
    /// <summary>
    ///     行驶记录
    /// </summary>
    public class RunRecord
    {
        public Guid Id { get; set; }
        public Guid Product_CarId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal StartLongitude { get; set; }
        public decimal EndLongitude { get; set; }
        public decimal StartLatitude { get; set; }
        public decimal EndLatitude { get; set; }
        public string StartPosition { get; set; }
        public string EndPosition { get; set; }
        public decimal StartMileage { get; set; }
        public decimal EndMileage { get; set; }
        public decimal StartFuel { get; set; }
        public decimal EndFuel { get; set; }
        public decimal AverageFuel { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal AverageSpeed { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}