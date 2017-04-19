using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    //公告
    public class Announcement
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key,Required]
        public int Id { set; get; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsEnable { get; set; }
    }
}
