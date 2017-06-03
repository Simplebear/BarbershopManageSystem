using BMS.Service;
using BMS.Utils.Enum;
using BMS.WebApi.filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.WebApi.Api.v1
{
    [RoutePrefix("api/v1/statistics")]
    public class StatisticsController : ApiController
    {
        StatisticsService _service = null;
        public StatisticsController()
        {
            _service = new StatisticsService();
        }
        [HttpGet, Route("loginCounts"), AuthValidater(RoleType.管理员)]
        public IHttpActionResult LoginCopunts()
        {
            try
            {
                return Ok(_service.LoginCount());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet, Route("inputCounts"), AuthValidater(RoleType.管理员)]
        public IHttpActionResult InputCounts()
        {
            try
            {
                return Ok(_service.InputCount());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
