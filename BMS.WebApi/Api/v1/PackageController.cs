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
    ///  套餐
    /// </summary>
    [RoutePrefix("api/v1/package")]
    public class PackageController : ApiController
    {
        PackageService packageService = null;
        public PackageController()
        {
            packageService = new PackageService();
        }
        /// <summary>
        /// 增加套餐
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route(""), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Add(PackageModel model)
        {
            try
            {
                return Ok(packageService.Add(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route(""), AllowAnonymous]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(packageService.Delete(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 修改套餐
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, Route(""), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Put(PackageModel model)
        {
            try
            {
                return Ok(packageService.Update(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
         /// <summary>
         /// 获取单个套餐信息
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet, Route(""), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(packageService.Get(id));
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
        [HttpGet, Route("all"), AllowAnonymous, ResponseType(typeof(ShareModel))]
        public IHttpActionResult GetAll(int pa)
        {
            try
            {
                return Ok(packageService.GetAll());
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
        public IHttpActionResult Search(string pageIndex = "1", string pageSize = "10")
        {
            try
            {
                return Ok(packageService.Search(int.Parse(pageIndex),int.Parse(pageSize)));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
