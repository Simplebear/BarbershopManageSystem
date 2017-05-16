using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    public class Order
    {
        [Key,Required]
        public int Id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 支付方式 支付宝，微信，线下
        /// </summary>
        public string ChanelCode { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { set; get; }
    }
}
