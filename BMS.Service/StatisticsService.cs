using BMS.Data;
using BMS.Model;
using BMS.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class StatisticsService
    {
        BMSDBContext Db = null;
        public List<ChartModel> LoginCount()
        {
            Db = new BMSDBContext();
            ChartModel model = null;
            List<ChartModel> results = new List<ChartModel>();
            var entities = Db.LoginHistory.OrderBy(o=>o.LoginTime).ToList();
            var date = DateTime.Now.Date;
            for (int i = 0; i < 7; i++)
            {
                model = new ChartModel();
                model.Key = date.AddDays(i-7);
                model.Value = entities.Where(o => o.LoginTime.Date == model.Key).Count();
                results.Add(model);
            }
            return results;
        }
        public List<ChartModel> InputCount()
        {
            Db = new BMSDBContext();
            ChartModel model = null;
            List<ChartModel> results = new List<ChartModel>();
            var entities = Db.Order.OrderBy(o=>o.CreatedOn).ToList();
            var date = DateTime.Now.Date;
            var status = OrderStatus.Completed.ToString();
            for (int i = 0; i < 7; i++)
            {
                model = new ChartModel();
                model.Key = date.AddDays(i - 7);
                model.Value = Convert.ToInt32(entities.Where(o => o.CreatedOn.Date == model.Key && o.OrderStatus == status).Sum(o=>o.Price));
                results.Add(model);
            }
            return results;
        }
    }
}
