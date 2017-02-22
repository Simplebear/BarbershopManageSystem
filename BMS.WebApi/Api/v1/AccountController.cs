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
                //this.ModelState.AddModelError(e.Message, e.ToString());
                //HttpError httpError = new HttpError(ModelState, true);
                //httpError.Message = "Internal Server Error";
                //throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, httpError));
                throw new Exception(e.Message);
            }
            
        }
    }
}
