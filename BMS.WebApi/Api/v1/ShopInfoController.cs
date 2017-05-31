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
    [RoutePrefix("api/v1/shopSetting")]
    public class ShopInfoController : ApiController
    {
        ShopSettingService shopSettingService = null;
        public ShopInfoController()
        {
            shopSettingService = new ShopSettingService();
            shopSettingService.UserId = Helper.UserId;
        }

        /// <summary>
        /// 修改店铺信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, Route(""), AllowAnonymous, ResponseType(typeof(ShopSettingModel))]
        public IHttpActionResult Put(ShopSettingModel model)
        {
            try
            {
                return Ok(shopSettingService.Update(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取店铺信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route(""), AllowAnonymous, ResponseType(typeof(ShopSettingModel))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(shopSettingService.Get());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
