using BMS.Model;
using BMS.Utils;
using BMS.Utils.Enum;
using BMS.WebApi.Api.v1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BMS.WebApi.filter
{
    public class AuthValidater : AuthorizeAttribute
    {
        public AuthValidater(params RoleType[] accountBookRoles)
        {
            this._accountBookRoles = accountBookRoles;
        }
        private RoleType[] _accountBookRoles;
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.FirstOrDefault(o => o.Key == "token").Value.FirstOrDefault();
            var json = EncryptHelper.DecryptString(token, "1qqqwww3");
            var user = JsonConvert.DeserializeObject<TokenModel>(json);
            Helper.UserId = user.UserId;
            int count = 0;
            foreach (var item in _accountBookRoles)
            {
                if (user.Role != item.ToString())
                {
                    count += 1;
                   
                }
            }
            if (count == 0)
            {
                throw new Exception("无权限访问");
            }
            return true;
        }
    }
}