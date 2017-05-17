using BMS.Data;
using BMS.Data.Entity;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Service
{
    public class OrderService
    {
        protected BMSDBContext Db = null;
        /// <summary>
        /// 预约
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public OrderModel Add(OrderModel orderModel)
        {
            using (Db = new BMSDBContext())
            {
                //获取店铺设置
                var companyInfo = Db.ShopSetting.FirstOrDefault();
                //1.判断是否在营业时间
                if (orderModel.StartTime.Hour < companyInfo.StartTime.Hour || orderModel.StartTime.Hour < companyInfo.EndTime.Hour)
                {
                    throw new Exception("所选时间不在营业时间");
                }
                //2.判断是否超过同时服务人数
                //服务所需分钟数
                var needTime = orderModel.Packages.Sum(o => o.Timespan);
                //服务结束时间
                var serveEndTime = orderModel.StartTime.AddMinutes(needTime);
                var count = (orderModel.StartTime - serveEndTime).Minutes / 30;
                for (int i = 1; i <= count; i++)
                {
                    var start = orderModel.StartTime.AddMinutes(needTime * (i - 1));
                    var end = orderModel.StartTime.AddMinutes(needTime * i);
                    var allShedulCounts = Db.Schedule.Where(o => o.StartTime <= orderModel.StartTime && o.EndTime >= end).Count();
                    if (allShedulCounts >= companyInfo.MaxServeCount)
                    {
                        throw new Exception(string.Format("{0}-{1}时间段店铺达到最大服务人数", start, end));
                    }
                }
                //3.判断理发师时间安排
                var shedulCounts = Db.Schedule.Where(o => o.BarberId == orderModel.BarberId && o.StartTime <= orderModel.StartTime && o.EndTime >= serveEndTime).Count();
                if (shedulCounts > 0)
                {
                    throw new Exception("与理发师时间冲突");
                }
                //插入数据
                var order = new Order()
                {
                    CustomerId = orderModel.UserId,
                    OrderNo = orderModel.OrderNo,
                    CreatedBy = orderModel.UserId,
                    CreatedOn = DateTime.Now,
                    OrderStatus = "",
                    Price = orderModel.Packages.Sum(o => o.Price),
                    ChanelCode = ""
                };
                Db.Order.Add(order);
                Db.SaveChanges();
                foreach (var item in orderModel.Packages)
                {
                    var orderPackage = new OrderPackage()
                    {
                        OrderId = Db.Order.Where(o=>o.OrderNo == orderModel.OrderNo).FirstOrDefault().Id,
                        PackageId = item.Id
                    };
                    Db.OrderPackage.Add(orderPackage);
                }
                var shedul = new Schedule()
                {
                    CustomerId = orderModel.UserId,
                    BarberId = orderModel.BarberId,
                    OrderId = Db.Order.Where(o => o.OrderNo == orderModel.OrderNo).FirstOrDefault().Id,
                    StartTime = orderModel.StartTime,
                    EndTime = serveEndTime,
                    CreatedOn = DateTime.Now
                };
                Db.Schedule.Add(shedul);
            }
            if (Db.SaveChanges()<1)
            {
                throw new Exception("错误");
            }
            return orderModel;
        }
    }
}
