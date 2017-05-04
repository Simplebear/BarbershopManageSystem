using BMS.Data;
using BMS.Data.Entity;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class AnnouncementService
    {
        BMSDBContext Db = null;
        public AnnouncementService()
        {
            Db = new BMSDBContext("name = DefaultConnection");
        }

        /// <summary>
        /// 获取全部公告列表
        /// </summary>
        /// <returns></returns>
        public List<AnnouncementModel> GetAll()
        {
            var results = new List<AnnouncementModel>();
            AnnouncementModel model = null;
            var entities = Db.Announcement.Where(o => o.IsEnable == true).ToList();
            if (entities!=null)
            {
                foreach (var entity in entities)
                {
                    model = new AnnouncementModel();
                    model.Id = entity.Id;
                    model.Content = entity.Content;
                    model.Title = entity.Title;
                    model.CreatedBy = entity.CreatedBy;
                    model.CreatedOn = entity.CreatedOn;
                    results.Add(model);
                }
            }           
            return results;
        }

        /// <summary>
        /// 根据Id获取公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AnnouncementModel Get(int id)
        {
            AnnouncementModel model = null;
            var entity = Db.Announcement.Where(o => o.Id == id).FirstOrDefault();
            model = new AnnouncementModel();
            model.Id = entity.Id;
            model.Content = entity.Content;
            model.Title = entity.Title;
            model.CreatedBy = entity.CreatedBy;
            model.CreatedOn = entity.CreatedOn;
            return model;
        }

        /// <summary>
        /// 根据Id删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            AnnouncementModel model = null;
            var entity = Db.Announcement.Where(o => o.Id == id).FirstOrDefault();
            if (entity!=null)
            {
                Db.Announcement.Remove(entity);
            }
            var count = Db.SaveChanges();
            if (count==0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 增加公告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AnnouncementModel Add(AnnouncementModel model)
        {
            Announcement entity = null;
            entity = new Announcement();
            entity.Id = model.Id;
            entity.Content = model.Content;
            entity.Title = model.Title;
            entity.CreatedBy = model.CreatedBy;
            entity.CreatedOn = model.CreatedOn;
            entity.IsEnable = true;
            Db.Announcement.Add(entity);
            Db.SaveChanges();
            return model;
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AnnouncementModel Update(AnnouncementModel model)
        {
            Announcement entity = null;
            entity = new Announcement();
            entity.Id = model.Id;
            entity.Content = model.Content;
            entity.Title = model.Title;
            entity.CreatedBy = model.CreatedBy;
            entity.CreatedOn = model.CreatedOn;
            entity.IsEnable = true;
            Db.Entry(entity).State = EntityState.Modified;
            Db.SaveChanges();
            return model;
        }
    }
}
