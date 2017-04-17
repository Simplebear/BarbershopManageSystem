using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string PresonalInfo { get; set; }

    }
}
