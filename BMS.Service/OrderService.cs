using BMS.Data;
using BMS.Data.Entity;
using BMS.Model;
using BMS.Utils;
using BMS.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                if (orderModel.StartTime.Hour < companyInfo.StartTime.Hour || orderModel.StartTime.Hour > companyInfo.EndTime.Hour)
                {
                    throw new Exception("所选时间不在营业时间");
                }
                //2.判断是否超过同时服务人数
                //服务所需分钟数
                var pacId = Convert.ToInt32(orderModel.Packages[0].Id); 
                var needTime = Db.Package.FirstOrDefault(o=>o.Id == pacId).Timespan;
                //服务结束时间
                var serveEndTime = orderModel.StartTime.AddMinutes(needTime);
                //判断有多少个半小时
                var count = (serveEndTime - orderModel.StartTime).TotalMinutes / 30;
                for (int i = 1; i <= count; i++)
                {
                    //30分钟遍历
                    var start = orderModel.StartTime.AddMinutes(30 * (i - 1));
                    var end = orderModel.StartTime.AddMinutes(30 * i);
                    //判断时间内有多少人
                    var allShedulCounts = Db.Schedule.Where(o => o.StartTime <= start && o.EndTime >= end).Count();
                    if (allShedulCounts >= companyInfo.MaxServeCount)
                    {
                        throw new Exception(string.Format("{0}-{1}时间段店铺达到最大服务人数", start, end));
                    }
                    //3.判断理发师时间安排
                    var shedulCounts = Db.Schedule.Where(o => o.BarberId == orderModel.BarberId && o.StartTime <= start && o.EndTime >= end).Count();
                    if (shedulCounts > 0)
                    {
                        throw new Exception("与理发师时间冲突");
                    }
                }

                var order = new Order()
                {
                    CustomerId = orderModel.UserId,
                    //随机生成
                    OrderNo = CommonHelper.GetRandomString(6),
                    CreatedBy = orderModel.UserId,
                    CreatedOn = DateTime.Now,
                    OrderStatus = OrderStatus.Ordered.ToString(),
                    Price = orderModel.Packages.Sum(o => o.Price),
                    ChanelCode = orderModel.Chanel.ToString()
                };
                orderModel.OrderNo = order.OrderNo;
                Db.Order.Add(order);
                Db.SaveChanges();
                foreach (var item in orderModel.Packages)
                {
                    var orderPackage = new OrderPackage()
                    {
                        OrderId = Db.Order.Where(o => o.OrderNo == orderModel.OrderNo).FirstOrDefault().Id,
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
                if (Db.SaveChanges() < 1)
                {
                    throw new Exception("错误");
                }
            }           
            return orderModel;
        }

        public PagedResult<OrderModel> Search(int pageIndex, int pageSize)
        {
            Db = new BMSDBContext();
            OrderModel model = null;
            List<OrderModel> models = new List<OrderModel>();
            Expression<Func<Order, bool>> filter = o => true;
            var totalRecord = 0;
            var list = Db.Order.Where(filter);
            totalRecord = list.Count();
            list = list.OrderByDescending(o => o.CreatedOn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var schedule = Db.Schedule.ToList();
            var packages = Db.Package.ToList();
            var orderPackage = Db.OrderPackage.ToList();
            foreach (var entity in list)
            {
                model = new OrderModel();
                model.Id = entity.Id;
                model.BarberId = schedule.Where(o=>o.OrderId == entity.Id).FirstOrDefault().BarberId;
                model.OrderNo = entity.OrderNo;
                model.StartTime = schedule.Where(o => o.OrderId == entity.Id).FirstOrDefault().StartTime;
                model.CreatedOn = entity.CreatedOn;
                model.Packages = new List<PackageModel>();
                var package = new PackageModel();
                var orderPac = orderPackage.Where(o => o.OrderId == model.Id).FirstOrDefault();
                var packageB = packages.Where(o => o.Id == orderPac.PackageId).FirstOrDefault();
                package.Id = packageB.Id;
                package.Name = packageB.Name;
                package.Description = package.Description;
                package.Price = package.Price;
                models.Add(model);
            }
            return new PagedResult<OrderModel>(pageIndex, pageSize, totalRecord, models);
        }

        public OrderModel UpdateStatus(OrderModel model)
        {
            if (model == null)
            {
                throw new Exception("Model为空");
            }
            using (Db = new BMSDBContext())
            {
                var entity = Db.Order.Where(o => o.Id == model.Id).FirstOrDefault();
                if (entity != null)
                {
                    entity.OrderStatus = model.OrderStatus;
                }
                Db.SaveChanges();
            }
            return model;
        }
    }
}
