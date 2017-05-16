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
    }
}
