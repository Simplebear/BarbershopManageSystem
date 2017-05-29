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
        /// 服务中
        /// </summary>
        serving,
        /// <summary>
        /// 已关闭
        /// </summary>
        closed,
        /// <summary>
        /// 已完成
        /// </summary>
        complete,
        /// <summary>
        /// 已预订
        /// </summary>
        Ordered
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
