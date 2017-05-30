using BMS.Data;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class ShopSettingService
    {
        BMSDBContext Db = null;

        public ShopSettingModel Update(ShopSettingModel model)
        {
            using (Db = new BMSDBContext())
            {
                var entity = Db.ShopSetting.FirstOrDefault();
                entity.StartTime = model.StartTime;
                entity.EndTime = model.EndTime;
                entity.Name = model.Name;
                Db.Entry(entity).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return model;
        }

        public ShopSettingModel Get()
        {
            ShopSettingModel model = new ShopSettingModel();
            using (Db = new BMSDBContext())
            {
                var entity = Db.ShopSetting.FirstOrDefault();
                model.StartTime = entity.StartTime;
                model.EndTime = entity.EndTime;
                model.Name = entity.Name;
                model.IsBusiness = entity.IsBusiness;
                model.MaxServeCount = entity.MaxServeCount;
            }
            return model;
        }
    }
}
