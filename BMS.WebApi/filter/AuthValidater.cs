using BMS.Model;
using BMS.Utils;
using BMS.Utils.Enum;
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
            var json = EncryptHelper.DecryptString(token, "1qqqwww2");
            var user = JsonConvert.DeserializeObject<TokenModel>(json);
            foreach (var item in _accountBookRoles)
            {
                if (user.Role != item.ToString())
                {
                    throw new Exception("无权限访问");
                }
            }
            return true;
        }
    }
}