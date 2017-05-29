using BMS.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Model
{
    public class OrderModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 所选套餐
        /// </summary>
        public List<PackageModel> Packages { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 理发师Id
        /// </summary>
        public int BarberId { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 订单创建人
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 订单创建人
        /// </summary>
        public Chanel Chanel { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { set; get; }
    }
}
