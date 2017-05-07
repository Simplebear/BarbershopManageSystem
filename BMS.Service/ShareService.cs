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
    public class ShareService
    {
        BMSDBContext Db = null;
        public ShareModel Get(int id)
        {
            ShareModel model = new ShareModel();
            using (Db = new BMSDBContext())
            {
                var entity = Db.Share.Where(o => o.Id == id).FirstOrDefault();
                model.Id = entity.Id;
                model.UserId = entity.Id;
                model.ImageUrl = entity.ImageUrl;
                model.Content = entity.Content;
                model.CreatedOn = entity.CreatedOn;
            }
            return model;
        }
        public bool Delete(int id)
        {
            int result = 0;
            using (Db = new BMSDBContext())
            {
                var entity = Db.Share.Where(o => o.Id == id).FirstOrDefault();
                Db.Share.Remove(entity);
                result=Db.SaveChanges();
            }
            return bool.Parse(result.ToString());
        }
        public ShareModel Update(ShareModel model)
        {
            using (Db = new BMSDBContext())
            {
                var entity = Db.Share.Where(o => o.Id == model.Id).FirstOrDefault();
                model.Id = entity.Id;
                model.UserId = entity.Id;
                model.ImageUrl = entity.ImageUrl;
                model.Content = entity.Content;
                model.CreatedOn = entity.CreatedOn;
            }
            return model;
        }
        public ShareModel Add(ShareModel model)
        {
            using (Db = new BMSDBContext())
            {
                var entity = new Share();
                entity.Id = model.Id;
                entity.UserId = model.Id;
                entity.ImageUrl = model.ImageUrl;
                entity.Content = model.Content;
                entity.CreatedOn = DateTime.Now.Date;
                Db.Share.Add(entity);
            }
            return model;
        }
    }
   
}
