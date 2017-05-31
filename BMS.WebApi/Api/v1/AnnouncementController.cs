using BMS.Model;
using BMS.Service;
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
    /// 公告
    /// </summary>
    [RoutePrefix("api/v1/announcement")]
    public class AnnouncementController : ApiController
    {
        AnnouncementService accountService = null;
        public AnnouncementController()
        {
            accountService = new AnnouncementService();
            accountService.UserId = Helper.UserId;
        }
        [HttpGet, Route("all"), AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(accountService.GetAll());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpPost, Route(""), AllowAnonymous]
        public IHttpActionResult Add(AnnouncementModel model)
        {
            try
            {
                return Ok(accountService.Add(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpDelete, Route(""), AllowAnonymous]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(accountService.Delete(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut, Route(""), AllowAnonymous]
        public IHttpActionResult Put(AnnouncementModel model)
        {
            try
            {
                return Ok(accountService.Update(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet, Route(""), AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(accountService.Get(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet, Route("search"), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Search(string pageIndex = "1", string pageSize = "10")
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
