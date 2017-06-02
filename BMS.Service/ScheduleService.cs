using BMS.Data;
using BMS.Data.Entity;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class ScheduleService:BaseService
    {
        BMSDBContext Db = null;
        public List<ScheduleModel> GetSchedules()
        {
            Db = new BMSDBContext();
            var date = DateTime.Now.Date;
            var entities =Db.Schedule.Where(
                o => o.BarberId == UserId 
                && o.StartTime.Year == date.Year 
                && o.StartTime.Month == date.Month
                && o.StartTime.Day == date.Day).OrderBy(o=>o.StartTime).ToList();
            List<ScheduleModel> models = new List<ScheduleModel>();
            foreach (var entity in entities)
            {
                var model = new ScheduleModel();
                model.StartTime = entity.StartTime;
                model.EndTime = entity.EndTime;
                model.BarberId = entity.BarberId;
                model.OrderId = entity.OrderId;
                model.CustomerId = entity.CustomerId;
                models.Add(model);
            }
            return models;
        }

        public bool AddSchedule(LeaveModel model)
        {
            using (Db = new BMSDBContext())
            {
                var en = Db.Schedule.Where(o => o.BarberId == UserId && o.StartTime.Month == model.date.Month
                && o.StartTime.Day == model.date.Day).ToList();
                if (en!=null&& en.Count>0)
                {
                    throw new Exception("已存在预约 不能请假");
                }
                var shopInfo = Db.ShopSetting.FirstOrDefault();
                var startTime = model.date.AddHours(shopInfo.StartTime.Hour).AddMinutes(shopInfo.StartTime.Minute);
                var endTime = model.date.AddHours(shopInfo.EndTime.Hour).AddMinutes(shopInfo.StartTime.Minute);
                var entity = new Schedule()
                {
                    BarberId = UserId,
                    StartTime = startTime,
                    EndTime = endTime,
                    CreatedOn = DateTime.Now
                };
                Db.Schedule.Add(entity);
                Db.SaveChanges();
            }
            return true;
        }
        public bool ForBidenSchedule(ForModel model)
        {
            using (Db = new BMSDBContext())
            {
                var entity = new Schedule()
                {
                    BarberId = UserId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    CreatedOn = DateTime.Now
                };
                Db.Schedule.Add(entity);
                Db.SaveChanges();
            }
            return true;
        }
    }
}
