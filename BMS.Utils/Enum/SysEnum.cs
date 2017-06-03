using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Utils.Enum
{
    public enum OrderStatus
    {
        /// <summary>
        /// 已支付
        /// </summary>
        Paid,
        /// <summary>
        /// 已关闭
        /// </summary>
        Closed,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed,
        /// <summary>
        /// 未支付
        /// </summary>
        NoPay
    }

    public enum Chanel
    {
        /// <summary>
        /// 线上支付
        /// </summary>
        Online,
        /// <summary>
        /// 线下支付
        /// </summary>
        Offline
        
    }
    public enum RoleType
    {
        管理员,
        理发师,
        顾客
    }
}
