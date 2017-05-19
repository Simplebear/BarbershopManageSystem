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
    /// <summary>
    /// 账户
    /// </summary>
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
        [HttpPost, Route("register"), AllowAnonymous]
        public IHttpActionResult Register(RegisterModel registerModel)
        {
            AccountService accountService = new AccountService();
            try
            {
                return Ok(accountService.Register(registerModel));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut, Route(""), AllowAnonymous]
        public IHttpActionResult Update(UserModel userModel)
        {
            AccountService accountService = new AccountService();
            try
            {
                return Ok(accountService.Update(userModel));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpGet, Route(""), AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            AccountService accountService = new AccountService();
            try
            {
                return Ok(accountService.Get(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpGet, Route("allBarber"), AllowAnonymous]
        public IHttpActionResult GetAllBarber()
        {
            AccountService accountService = new AccountService();
            try
            {
                return Ok(accountService.GetAllBarber());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
