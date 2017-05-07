using BMS.Model;
using BMS.Service;
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
        [HttpPost,Route("login"),AllowAnonymous]
        public IHttpActionResult test(LoginModel loginModel)
        {
            AccountService accountService = new AccountService();
            try
            {
                return Ok(accountService.Login(loginModel));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
