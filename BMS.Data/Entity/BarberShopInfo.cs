using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    public class BarberShopInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key,Required]
        public int Id { set; get; }
    }
}
