using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data.Entity
{
    public class OrderPackage
    {
        [Required]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int PackageId { get; set; }
    }
}
