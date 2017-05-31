using BMS.Data;
using BMS.Data.Entity;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class AnnouncementService
    {
        protected BMSDBContext Db = null;

        /// <summary>
        /// 获取全部公告列表(首页)
        /// </summary>
        /// <returns></returns>
        public List<AnnouncementModel> GetAll()
        {
            using (Db = new BMSDBContext())
            {
                var results = new List<AnnouncementModel>();
                AnnouncementModel model = null;
                var entities = Db.Announcement.Where(o => o.IsEnable == true).ToList();
                if (entities != null)
                {
                    foreach (var entity in entities)
                    {
                        model = new AnnouncementModel();
                        model.Id = entity.Id;
                        model.Content = entity.Content;
                        model.Title = entity.Title;
                        model.CreatedBy = entity.CreatedBy;
                        model.CreatedOn = entity.CreatedOn;
                        model.IsEnable = entity.IsEnable;
                        results.Add(model);
                    }
                }
                return results;
            }
                      
        }

        /// <summary>
        /// 根据Id获取公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AnnouncementModel Get(int id)
        {
            using (Db = new BMSDBContext())
            {
                AnnouncementModel model = null;
                var entity = Db.Announcement.Where(o => o.Id == id).FirstOrDefault();
                model = new AnnouncementModel();
                model.Id = entity.Id;
                model.Content = entity.Content;
                model.Title = entity.Title;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedOn = entity.CreatedOn;
                model.IsEnable = entity.IsEnable;
                return model;
            }            
        }

        /// <summary>
        /// 根据Id删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (Db = new BMSDBContext())
            {
                AnnouncementModel model = null;
                var entity = Db.Announcement.Where(o => o.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    Db.Announcement.Remove(entity);
                }
                var count = Db.SaveChanges();
                if (count == 0)
                {
                    return false;
                }
                return true;
            }         
        }

        /// <summary>
        /// 增加公告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AnnouncementModel Add(AnnouncementModel model)
        {
            using (Db = new BMSDBContext())
            {
                Announcement entity = null;
                entity = new Announcement();
                entity.Content = model.Content;
                entity.Title = model.Title;
                entity.CreatedBy = model.CreatedBy;
                entity.CreatedOn = DateTime.Now;
                entity.IsEnable = model.IsEnable;
                Db.Announcement.Add(entity);
                Db.SaveChanges();
                return model;
            }
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AnnouncementModel Update(AnnouncementModel model)
        {
            using (Db = new BMSDBContext())
            {
                Announcement entity = null;
                entity = new Announcement();
                entity = Db.Announcement.Where(o => o.Id == model.Id).FirstOrDefault();
                entity.Content = model.Content;
                entity.Title = model.Title;
                entity.CreatedBy = model.CreatedBy;
                entity.CreatedOn = DateTime.Now;
                entity.IsEnable = model.IsEnable;
                Db.Entry(entity).State = EntityState.Modified;
                Db.SaveChanges();
                return model;
            }
        }

        public PagedResult<AnnouncementModel> Search(int pageIndex, int pageSize)
        {
            Db = new BMSDBContext();
            AnnouncementModel model = null;
            List<AnnouncementModel> models = new List<AnnouncementModel>();
            Expression<Func<Announcement, bool>> filter = o => true;
            var totalRecord = 0;
            var list = Db.Announcement.Where(filter);
            totalRecord = list.Count();
            list = list.OrderByDescending(o => o.CreatedOn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            foreach (var entity in list)
            {
                model = new AnnouncementModel();
                model.Id = entity.Id;
                model.Content = entity.Content;
                model.Title = entity.Title;
                model.CreatedBy = entity.CreatedBy;
                model.CreatedOn = entity.CreatedOn;
                model.IsEnable = entity.IsEnable;
                models.Add(model);
            }
            return new PagedResult<AnnouncementModel>(pageIndex, pageSize, totalRecord, models);
        }
    }
}
