using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    /// <summary>
    /// 分享表
    /// </summary>
    public class Share
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key,Required]
        public  int Id { get; set; }

        /// <summary>
        /// 分享内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分享人
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDeleted { get; set; }
    }
}
