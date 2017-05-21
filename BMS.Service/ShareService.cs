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
                model.UserId = entity.UserId;
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
                entity.Content = entity.Content;
                entity.ImageUrl = model.ImageUrl;
                Db.Entry(entity).State = EntityState.Modified;
                Db.SaveChanges();
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
                Db.SaveChanges();
            }
            return model;
        }

        /// <summary>
        /// 用户获取个人所有分享
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ShareModel> GetUserAll(int id)
        {
            List<ShareModel> models = new List<ShareModel>();
            using (Db = new BMSDBContext())
            {             
                var entitys = Db.Share.Where(o=>o.UserId == id).ToList().OrderByDescending(o=>o.CreatedOn);
                foreach (var item in entitys)
                {
                    var model = new ShareModel();
                    model.Id = item.Id;
                    model.UserId = item.UserId;
                    model.ImageUrl = item.ImageUrl;
                    model.Content = item.Content;
                    model.CreatedOn = item.CreatedOn;
                    models.Add(model);
                }              
            }
            return models;
        }

        /// <summary>
        /// 分页获取分享
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public  PagedResult<ShareModel> Search(int pageIndex, int pageSize)
        {
            Db = new BMSDBContext();
            List<ShareModel> models = new List<ShareModel>();
            Expression<Func<Share, bool>> filter = o => true && o.IsDeleted == "N";
            var totalRecord = 0;
            var list = Db.Share.Where(filter);
            totalRecord = list.Count();
            list = list.OrderByDescending(o=>o.CreatedOn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            foreach (var item in list)
            {
                var model = new ShareModel();
                model.Id = item.Id;
                model.UserId = item.UserId;
                model.ImageUrl = item.ImageUrl;
                model.Content = item.Content;
                model.CreatedOn = item.CreatedOn;
                models.Add(model);
            }
            return new PagedResult<ShareModel>(pageIndex, pageSize, totalRecord,models);
        }
    }
   
}
