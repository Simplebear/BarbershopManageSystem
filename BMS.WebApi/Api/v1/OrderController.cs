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
    /// 支付
    /// </summary>
    [RoutePrefix("api/v1/order")]
    public class OrderController : ApiController
    {
        OrderService orderService = null;
        public OrderController()
        {
            orderService = new OrderService();
        }

        [HttpPost, Route(""), AllowAnonymous]
        public IHttpActionResult Add(OrderModel model)
        {
            try
            {
                return Ok(orderService.Add(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 分页获取所有订单
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Route("search"), AllowAnonymous]
        public IHttpActionResult Search(string pageIndex = "1", string pageSize = "10")
        {
            try
            {
                return Ok(orderService.Search(int.Parse(pageIndex), int.Parse(pageSize)));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, Route(""), AllowAnonymous]
        public IHttpActionResult Put(OrderModel model)
        {
            try
            {
                return Ok(orderService.UpdateStatus(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
