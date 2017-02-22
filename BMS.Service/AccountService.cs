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
        public string Login(LoginModel loginModel)
        {
            BMSDBContext Db = new BMSDBContext();
            var user = Db.User.Where(o => o.Account == loginModel.Account).FirstOrDefault();
            if (user==null)
            {
                return "账号未注册";
            }
            if (user.Password!=loginModel.Password)
            {
                return "密码错误";
            }
            return "登陆成功";
        }
    }
}
