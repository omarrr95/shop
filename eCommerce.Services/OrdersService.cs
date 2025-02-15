using eCommerce.Data;
using eCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class OrdersService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public OrdersService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public bool SaveOrder(Order order)
        {
            

            _eCommerceContext.Orders.Add(order);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public Order GetOrderByID(int ID)
        {
            

            return _eCommerceContext.Orders.Include("OrderItems.Product.ProductRecords").FirstOrDefault(x=>x.ID == ID);
        }

        public List<Order> SearchOrders(string userID, int? orderID, int? orderStatus, int? pageNo, int recordSize, out int count)
        {
            

            var orders = _eCommerceContext.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(userID))
            {
                orders = orders.Where(x => x.CustomerID.Equals(userID));
            }

            if (orderID.HasValue && orderID.Value > 0)
            {
                orders = orders.Where(x => x.ID == orderID.Value);
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                orders = orders.Where(x => x.OrderHistory.OrderByDescending(y => y.ModifiedOn).FirstOrDefault().OrderStatus == orderStatus);
            }

            count = orders.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return orders.OrderByDescending(x => x.PlacedOn).Skip(skipCount).Take(recordSize).ToList();
        }

        public bool AddOrderHistory(OrderHistory orderHistory)
        {
            

            _eCommerceContext.OrderHistories.Add(orderHistory);

            return _eCommerceContext.SaveChanges() > 0;
        }
    }
}
