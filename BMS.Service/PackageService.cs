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
    public class PackageService:BaseService
    {
        BMSDBContext Db = null;
        public PackageModel Get(int id)
        {
            PackageModel model = new PackageModel();
            using (Db = new BMSDBContext())
            {
                var entity = Db.Package.Where(o => o.Id == id).FirstOrDefault();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Price = entity.Price;
                model.Timespan = entity.Timespan;
                model.CreatedOn = entity.CreatedOn;
                model.IsDeleted = entity.IsDeleted;
                model.IsEnable = entity.IsEnable;
                model.Description = entity.Description;
            }
            return model;
        }
        public bool Delete(int id)
        {
            int result = 0;
            using (Db = new BMSDBContext())
            {
                var entity = Db.Package.Where(o => o.Id == id).FirstOrDefault();
                Db.Package.Remove(entity);
                result = Db.SaveChanges();
            }
            return bool.Parse(result.ToString());
        }
        public PackageModel Update(PackageModel model)
        {
            using (Db = new BMSDBContext())
            {
                var entity = Db.Package.Where(o => o.Id == model.Id).FirstOrDefault();
                entity.Description = model.Description;
                entity.IsDeleted = model.IsDeleted;
                entity.IsEnable = model.IsEnable;
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Timespan = model.Timespan;
                Db.Entry(entity).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return model;
        }
        public PackageModel Add(PackageModel model)
        {
            using (Db = new BMSDBContext())
            {
                var entity = new Package();
                entity.Description = model.Description;
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.IsDeleted = "N";
                entity.IsEnable = "Y";
                entity.Timespan = model.Timespan;
                entity.CreatedOn = DateTime.Now;
                Db.Package.Add(entity);
                Db.SaveChanges();
            }
            return model;
        }
        /// <summary>
        /// 获取所有套餐（用户预订用）
        /// </summary>
        /// <returns></returns>
        public List<PackageModel> GetAll()
        {
            List<PackageModel> models = new List<PackageModel>();
            using (Db = new BMSDBContext())
            {
                var entitys = Db.Package.ToList().OrderByDescending(o => o.CreatedOn);
                foreach (var item in entitys)
                {
                    var model = new PackageModel();
                    model.Id = item.Id;
                    model.Name = item.Name;
                    model.Price = item.Price;
                    model.Timespan = item.Timespan;
                    model.CreatedOn = item.CreatedOn;
                    model.IsDeleted = item.IsDeleted;
                    model.IsEnable = item.IsEnable;
                    model.Description = item.Description;
                    models.Add(model);
                }
            }
            return models;
        }
        /// <summary>
        /// 分页获取套餐（管理员）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public PagedResult<PackageModel> Search(int pageIndex, int pageSize)
        {
            Db = new BMSDBContext();
            List<PackageModel> models = new List<PackageModel>();
            Expression<Func<Package, bool>> filter = o => true && o.IsDeleted == "N";
            var totalRecord = 0;
            var list = Db.Package.Where(filter);
            totalRecord = list.Count();
            list = list.OrderByDescending(o => o.CreatedOn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            foreach (var item in list)
            {
                var model = new PackageModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.Price = item.Price;
                model.Timespan = item.Timespan;
                model.CreatedOn = item.CreatedOn;
                model.IsDeleted = item.IsDeleted;
                model.IsEnable = item.IsEnable;
                model.Description = item.Description;
                models.Add(model);
            }
            return new PagedResult<PackageModel>(pageIndex, pageSize, totalRecord, models);
        }
    }
}
