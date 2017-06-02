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
    [RoutePrefix("api/v1/schedule")]
    public class ScheduleController : ApiController
    {
        ScheduleService scheduleService = null;
        public ScheduleController()
        {
            scheduleService = new ScheduleService();
            scheduleService.UserId = Helper.UserId;
        }
        /// <summary>
        /// 请假
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route(""), AuthValidater(RoleType.理发师)]
        public IHttpActionResult Add(LeaveModel model)
        {
            try
            {
                return Ok(scheduleService.AddSchedule(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut, Route(""), AuthValidater(RoleType.理发师)]
        public IHttpActionResult Forbiden(ForModel model)
        {
            try
            {
                return Ok(scheduleService.ForBidenSchedule(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet, Route("all"), AuthValidater(RoleType.理发师)]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(scheduleService.GetSchedules());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
