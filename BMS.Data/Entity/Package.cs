using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    public class Package
    {
        [Key,Required]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 需要时间
        /// </summary>
        public int Timespan { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string IsEnable { get; set; }

        public string IsDeleted { get; set; }
    }
}
