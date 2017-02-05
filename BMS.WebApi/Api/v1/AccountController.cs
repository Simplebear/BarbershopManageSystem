using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.WebApi.Api.v1
{
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {
        [HttpGet,Route("test")]
        public string test(string a)
        {
            return a;
        }
    }
}
