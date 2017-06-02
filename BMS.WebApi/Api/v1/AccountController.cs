using BMS.Model;
using BMS.Service;
using BMS.Utils.Enum;
using BMS.WebApi.filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BMS.WebApi.Api.v1
{
    /// <summary>
    /// 账户
    /// </summary>
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {
        AccountService accountService = null;
        public AccountController()
        {
            accountService = new AccountService();
            accountService.UserId = Helper.UserId;
        }
       [HttpPost,Route("login"),AllowAnonymous]
        public IHttpActionResult Login(LoginModel loginModel)
        {
            try
            {
                
                return Ok(accountService.Login(loginModel));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        [HttpPost, Route("register")]
        public IHttpActionResult Register(RegisterModel registerModel)
        {
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
        [HttpPut, Route(""),AuthValidater(RoleType.顾客,RoleType.理发师)]
        public IHttpActionResult Update(UserModel userModel)
        {
            try
            {
                return Ok(accountService.Update(userModel));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route(""), AuthValidater(RoleType.理发师, RoleType.管理员, RoleType.顾客)]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(accountService.Get());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 获取所有理发师
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("allBarber"), AllowAnonymous]
        public IHttpActionResult GetAllBarber()
        {
            try
            {
                return Ok(accountService.GetAllBarber());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
         /// <summary>
         /// 新增理发师
         /// </summary>
         /// <param name="barber"></param>
         /// <returns></returns>
        [HttpPost, Route("barber"), AuthValidater(RoleType.管理员)]
        public IHttpActionResult AddBarber(UserModel barber)
        {
            try
            {
                return Ok(accountService.Add(barber));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 分页获取所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("search"), AuthValidater(RoleType.管理员), ResponseType(typeof(ShareModel))]
        public IHttpActionResult Search(string pageIndex = "1", string pageSize = "5")
        {
            try
            {
                return Ok(accountService.Search(int.Parse(pageIndex), int.Parse(pageSize)));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
