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
    /// 分享
    /// </summary>
    [RoutePrefix("api/v1/share")]
    public class ShareController : ApiController
    {
        ShareService shareService = null;
        public ShareController()
        {
            shareService = new ShareService();
        }

        [HttpPost, Route(""), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Add(ShareModel model)
        {
            try
            {
                return Ok(shareService.Add(model));
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
                return Ok(shareService.Delete(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut, Route(""), AllowAnonymous,ResponseType(typeof(ShareModel))]
        public IHttpActionResult Put(ShareModel model)
        {
            try
            {
                return Ok(shareService.Update(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet, Route(""), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(shareService.Get(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
