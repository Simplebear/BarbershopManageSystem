using BMS.Data;
using BMS.Data.Entity;
using BMS.Model;
using BMS.Utils;
using BMS.WebApi.Api.v1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BMS.Service
{
    public class ShareService: BaseService
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
                model.User.Id = entity.UserId;
                model.User.Name = Db.User.Where(o => o.Id == entity.Id).FirstOrDefault().Name;
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
            return true;
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
                entity.UserId = UserId;
                entity.ImageUrl = model.ImageUrl;
                entity.Content = model.Content;
                entity.CreatedOn = DateTime.Now.Date;
                entity.IsDeleted = "N";
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
        public List<ShareModel> GetUserAll()
        {
            List<ShareModel> models = new List<ShareModel>();
          
            using (Db = new BMSDBContext())
            {             
                var entitys = Db.Share.Where(o=>o.UserId == Helper.UserId).ToList().OrderByDescending(o=>o.CreatedOn);
                var users = Db.User.ToList();
                foreach (var item in entitys)
                {
                    var model = new ShareModel();
                    model.Id = item.Id;
                    model.UserId = item.UserId;
                    model.User.Id = item.UserId;
                    model.User.Name = users.Where(o => o.Id == item.UserId).FirstOrDefault().Name;
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
        public  PagedResult<ShareModel> Search(string keyWord,int pageIndex, int pageSize)
        {
            Db = new BMSDBContext();
            List<ShareModel> models = new List<ShareModel>();
            Expression<Func<Share, bool>> filter = o => true && o.IsDeleted == "N";
            var users = Db.User.ToList();
            if (keyWord != null)
            {
                filter = o => true && o.Content.Contains(keyWord) && o.IsDeleted == "N";
            }
            var totalRecord = 0;
            var list = Db.Share.Where(filter);
            totalRecord = list.Count();
            list = list.OrderByDescending(o=>o.CreatedOn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            foreach (var item in list)
            {
                var model = new ShareModel();
                model.Id = item.Id;
                model.UserId = item.UserId;
                model.User.Id = item.UserId;
                model.User.Name = users.Where(o => o.Id == item.UserId).FirstOrDefault().Name;
                model.ImageUrl = item.ImageUrl;
                model.Content = item.Content;
                model.CreatedOn = item.CreatedOn;
                models.Add(model);
            }
            return new PagedResult<ShareModel>(pageIndex, pageSize, totalRecord,models);
        }

        public string Upload(HttpRequestMessage request, int? Id)
        {
            var path = CreateUploadFolder("img");
            var text = request.Content.ReadAsStringAsync();
            string suffix = ".jpg"; //文件的后缀名根据实际情况
            var name = Guid.NewGuid().ToString();
            string fullpath = path + "\\"+ name + suffix;
            Base64ToImg(text.Result.Split(',')[1]).Save(fullpath); 
            var returnPath = string.Concat("http://localhost:11162/temp/img/",name,suffix);
            return returnPath;
        }
        private string CreateUploadFolder(string location)
        {
            //string root = HttpContext.Current.Server.MapPath("~/Upload");
            DateTime dtNow = DateTime.UtcNow;
            var root = Path.Combine(HttpRuntime.AppDomainAppPath, "temp\\" + location);
            var uploadFolder = root;
            try
            {
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateUploadFolder");
            }
            return uploadFolder;
        }
        //解析base64编码获取图片
        private Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }
    }
   
}
