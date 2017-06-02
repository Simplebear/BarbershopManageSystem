using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Model
{
    public class AnnouncementModel
    {
        public AnnouncementModel()
        {
            CreateUser = new IdNameModel();
        }
        public int Id { set; get; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CreatedBy { get; set; }

        public IdNameModel CreateUser { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsEnable { get; set; }
    }
}
