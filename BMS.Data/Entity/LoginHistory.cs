using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    /// <summary>
    /// 登录历史
    /// </summary>
    public class LoginHistory
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key,Required]
        public int Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }
}
