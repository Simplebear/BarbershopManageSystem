using BMS.Data;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class AccountService
    {
        BMSDBContext Db = null;
        public UserModel Login(LoginModel loginModel)
        {
            UserModel userModel = new UserModel();
            using (Db = new BMSDBContext())
            {
                var user = Db.User.Where(o => o.PhoneNumber == loginModel.Account).FirstOrDefault();
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.ImageUrl = user.PhotoUrl;
                var userRole = Db.UserRole.Where(o => o.UserId == user.Id).FirstOrDefault();
                var role = Db.Role.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                userModel.Role = new IdNameModel() { Id = role.Id, Name = role.Name };
            }
            return userModel;                        
        }

        public UserModel Register(RegisterModel registerModel)
        {
            UserModel userModel = new UserModel();
            using (Db = new BMSDBContext())
            {
                var user = new User();
                user.Name = registerModel.Name;
                user.Password = registerModel.Password;
                user.PhoneNumber = registerModel.PhoneNumber;
                user.Email = registerModel.Email;
                Db.User.Add(user);
                Db.SaveChanges();
                var userRole = new UserRole();
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
                var user = Db.User.Where(o=>o.Id == userModel.Id).FirstOrDefault();
                user.Name = userModel.Name;
                user.PhoneNumber = userModel.PhoneNumber;
                user.Email = userModel.Email;
                Db.User.Add(user);
                //设置角色
                Db.SaveChanges();
            }
            return userModel;
        }
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public UserModel Get(int id)
        {
            var userModel = new UserModel();
            using (Db = new BMSDBContext())
            {
                var user = Db.User.Where(o => o.Id == id).FirstOrDefault();
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.ImageUrl = user.PhotoUrl;
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
                userModel.Name = user.Name;
                userModel.PhoneNumber = user.PhoneNumber;
                userModel.Email = user.Email;
                userModel.ImageUrl = user.PhotoUrl;
                var userRole = Db.UserRole.Where(o => o.UserId == user.Id).FirstOrDefault();
                var role = Db.Role.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                userModel.Role = new IdNameModel() { Id = role.Id, Name = role.Name };
                userModels.Add(userModel);
            }
            return userModels;
        }

    }
}
