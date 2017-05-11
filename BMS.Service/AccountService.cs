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
                user.Id = Db.User.Select(o => o.Id).Max() + 1;
                user.Name = registerModel.Name;
                user.Password = registerModel.Password;
                user.PhoneNumber = registerModel.PhoneNumber;
                user.Email = registerModel.Email;
                Db.User.Add(user);
                var userRole = new UserRole();
                userRole.UserId = user.Id;
                userRole.RoleId = 3;
                userRole.CreatedOn = DateTime.Now;
                Db.UserRole.Add(userRole);
                //设置角色
                Db.SaveChanges();
                userModel.Name = registerModel.Name;
                userModel.PhoneNumber = registerModel.PhoneNumber;
                userModel.Email = registerModel.Email;
                userModel.Role = new IdNameModel() { Id = 3, Name = "管理员" };
            }
            return userModel;
        }
    }
}
