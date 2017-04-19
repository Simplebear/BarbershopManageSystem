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

        public string Timespan { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string IsEnable { get; set; }

        public string IsDeleted { get; set; }
    }
}
