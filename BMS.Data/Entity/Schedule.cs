using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    /// <summary>
    /// 时间安排表
    /// </summary>
    public class Schedule
    {
        [Key,Required]
        public int Id { get; set; }

        /// <summary>
        /// 顾客Id
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// 理发师Id
        /// </summary>
        public int BarberId { get; set; }

        /// <summary>
        /// 关联的订单Id
        /// </summary>
        public int? OrderId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
