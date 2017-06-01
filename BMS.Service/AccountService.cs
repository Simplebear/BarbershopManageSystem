using BMS.Data;
using BMS.Model;
using BMS.Utils;
using Newtonsoft.Json;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BMS.Data.Entity;
using BMS.WebApi.Api.v1;

namespace BMS.Service
{
    public class AccountService: BaseService
    {
        private static readonly string EncryptionKey = "1qqqwww3";
        BMSDBContext Db = null;
        public UserModel Login(LoginModel loginModel)
        {
            UserModel userModel = new UserModel();
            using (Db = new BMSDBContext())
            {
                var user = Db.User.Where(o => (o.PhoneNumber == loginModel.Account|| o.Email == loginModel.Account) && o.Password == loginModel.Password).FirstOrDefault();
                userModel.Id = user.Id;
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.ImageUrl = user.PhotoUrl;
                var userRole = Db.UserRole.Where(o => o.UserId == user.Id).FirstOrDefault();
                var role = Db.Role.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                userModel.Role = new IdNameModel() { Id = role.Id, Name = role.Name };
                //登录历史
                LoginHistory his = new LoginHistory();
                his.UserId = user.Id;
                his.LoginTime = DateTime.Now;
                Db.LoginHistory.Add(his);
                Db.SaveChanges();
            }
            var tokenModel = new TokenModel()
            {
                UserId = userModel.Id,
                Name = userModel.Name,
                Role = userModel.Role.Name
            };
            var value = EncryptHelper.EncryptString(JsonConvert.SerializeObject(tokenModel), EncryptionKey);
            userModel.Token = value;
            return userModel;                        
        }

        public UserModel Register(RegisterModel registerModel)
        {
            UserModel userModel = new UserModel();
            var userRole = new UserRole();
            var user = new User();
            using (Db = new BMSDBContext())
            {
                var validater = Db.User.Where(o => o.PhoneNumber == registerModel.PhoneNumber).FirstOrDefault();
                if (validater != null)
                {
                    throw new Exception("手机号已存在");
                }              
                user.Name = registerModel.Name;
                user.Password = registerModel.Password;
                user.PhoneNumber = registerModel.PhoneNumber;
                user.Email = registerModel.Email;
                Db.User.Add(user);
                Db.SaveChanges();              
                userRole.UserId = Db.User.Where(o=>o.PhoneNumber == user.PhoneNumber).FirstOrDefault().Id;
                userRole.RoleId = 1;
                userRole.CreatedOn = DateTime.Now;
                Db.UserRole.Add(userRole);
                //设置角色
                Db.SaveChanges();
                userModel.Id = Db.User.Where(o => o.PhoneNumber == registerModel.PhoneNumber).FirstOrDefault().Id;
                userModel.Name = registerModel.Name;
                userModel.PhoneNumber = registerModel.PhoneNumber;
                userModel.Email = registerModel.Email;
                userModel.Role = new IdNameModel() { Id = 1, Name = "顾客" };
            }
            return userModel;
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public UserModel Update(UserModel userModel)
        {
            using (Db = new BMSDBContext())
            {
                //var validater = Db.User.FirstOrDefault(o => o.PhoneNumber == userModel.PhoneNumber).Id;
                //if (validater!=null)
                //{
                //    throw new Exception("手机号已存在");
                //}
                //var validater = userModel.Id;
                var user = Db.User.Where(o=>o.Id == UserId).FirstOrDefault();
                user.Name = userModel.Name;
                user.PhoneNumber = userModel.PhoneNumber;
                user.Email = userModel.Email;
                Db.Entry(user).State = EntityState.Modified;
                //Db.User.Add(user);
                Db.SaveChanges();
                return userModel;
            }
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public UserModel Get()
        {
            var userModel = new UserModel();
            using (Db = new BMSDBContext())
            {
                var user = Db.User.Where(o => o.Id == Helper.UserId).FirstOrDefault();
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.PersonalInfo = user.PresonalInfo;
                userModel.ImageUrl = user.PhotoUrl;
                userModel.Id = user.Id;
                var userRole = Db.UserRole.Where(o => o.UserId == user.Id).FirstOrDefault();
                var role = Db.Role.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                userModel.Role = new IdNameModel() { Id = role.Id, Name = role.Name };
            }
            return userModel;
        }

        public List<UserModel> GetAllBarber()
        {
            Db = new BMSDBContext();
            var userModels = new List<UserModel>();
            var ids = Db.UserRole.Where(o=>o.RoleId == 2).Select(o=>o.UserId).ToList();
            foreach (var id in ids)
            {
                var user =Db.User.Where(o => o.Id == id).FirstOrDefault();
                var userModel = new UserModel();
                userModel.Id = user.Id;
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.ImageUrl = user.PhotoUrl;
                userModel.PersonalInfo = user.PresonalInfo;
                var userRole = Db.UserRole.Where(o => o.UserId == user.Id).FirstOrDefault();
                var role = Db.Role.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                userModel.Role = new IdNameModel() { Id = role.Id, Name = role.Name };
                userModels.Add(userModel);
            }
            return userModels;
        }

        public UserModel Add(UserModel Barber)
        {
            using (Db = new BMSDBContext()) //一次数据库连接
            {
                var validater = Db.User.Where(o => o.PhoneNumber == Barber.PhoneNumber).FirstOrDefault();
                if (validater != null)
                {
                    throw new Exception("手机号已存在");
                }
                var entity = new User(); //建一条user记录
                entity.Name = Barber.Name;
                entity.PhoneNumber = Barber.PhoneNumber;
                entity.Email = Barber.Email;
                entity.PresonalInfo = Barber.PersonalInfo;
                entity.Password = "123456";
                Db.User.Add(entity);
                Db.SaveChanges(); //保存
                var userRole = new UserRole();
                userRole.UserId = Db.User.Where(o => o.PhoneNumber == Barber.PhoneNumber).FirstOrDefault().Id;
                userRole.RoleId = 2;
                Db.UserRole.Add(userRole);
                Db.SaveChanges();
                Barber.Id = userRole.UserId;
            }
            return Barber;
        }

        public PagedResult<UserModel> Search(int pageIndex, int pageSize)
        {
            Db = new BMSDBContext();
            var models = new List<UserModel>();

            var userRoles = Db.UserRole.ToList();
            var roles = Db.Role.ToList();
            Expression<Func<User, bool>> filter = o => true;
            var totalRecord = 0;
            var list = Db.User.Where(filter);
            totalRecord = list.Count();
            list = list.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            foreach (var user in list)
            {
                var userModel = new UserModel();
                userModel.Id = user.Id;
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.ImageUrl = user.PhotoUrl;
                userModel.PersonalInfo = user.PresonalInfo;
                var userRole = userRoles.Where(o => o.UserId == user.Id).FirstOrDefault();
                var role = roles.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                userModel.Role = new IdNameModel() { Id = role.Id, Name = role.Name };
                models.Add(userModel);
            }
            return new PagedResult<UserModel>(pageIndex, pageSize, totalRecord, models);
        }
    }
}
