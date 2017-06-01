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
    /// 分享
    /// </summary>
    [RoutePrefix("api/v1/share")]
    public class ShareController : ApiController
    {
        ShareService shareService = null;
        public ShareController()
        {
            shareService = new ShareService();
            shareService.UserId = Helper.UserId;
        }
        /// <summary>
        /// 增加分享（顾客）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 删除分享（顾客）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 修改待定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获取分享
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获取个人所有分享
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("user/all"), AllowAnonymous]
        public IHttpActionResult GetUserAll()
        {
            try
            {
                return Ok(shareService.GetUserAll());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取所有套餐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("search"), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Search(string keyWord = null,string pageIndex = "1", string pageSize = "10")
        {
            try
            {
                return Ok(shareService.Search(keyWord,int.Parse(pageIndex), int.Parse(pageSize)));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost, Route("upload"), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Upload(int? id)
        {
            try
            {
                return Ok(shareService.Upload(Request,id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
