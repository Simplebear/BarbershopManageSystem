using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Model
{
    public class ShareModel
    {
        /// <summary>
        ///  主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分享内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分享内容
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 分享人
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
