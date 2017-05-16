using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    public class ShopSetting
    {
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 营业开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 营业结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 同时间段最多服务人数
        /// </summary>
        public int MaxServeCount { get; set; }
        
        /// <summary>
        /// 是否营业
        /// </summary>
        public bool IsBusiness { get; set; }
    }
}
