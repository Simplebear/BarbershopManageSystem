using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Model
{
    public class PackageModel
    {
        /// <summary>
        /// 套餐Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 套餐所消耗时间
        /// </summary>
        public int Timespan { get; set; }

        /// <summary>
        /// 套餐价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 是否处于启用状态
        /// </summary>
        public string IsEnable { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDeleted { get; set; }
    }
}
