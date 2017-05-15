﻿using BMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.WebApi.Api.v1
{
    [RoutePrefix("api/v1/order")]
    public class OrderController : ApiController
    {
        OrderService orderService = null;
        public OrderController()
        {
            orderService = new OrderService();
        }
    }
}
