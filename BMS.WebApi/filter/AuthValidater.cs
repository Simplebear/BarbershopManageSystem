using BMS.Model;
using BMS.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BMS.WebApi.filter
{
    public class AuthValidater : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.FirstOrDefault(o => o.Key == "token").Value.FirstOrDefault();
            var json = EncryptHelper.DecryptString(token, "1qqqwww2");
            var user = JsonConvert.DeserializeObject<TokenModel>(json);
        }
    }
}