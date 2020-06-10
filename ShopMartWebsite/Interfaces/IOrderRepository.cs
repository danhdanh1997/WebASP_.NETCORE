using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(int id);
        IEnumerable<Order> SearchOrders(string searchTerm, DateTime? searchDate, int page, int recordSize);

        //bool UpdateProduct(Product product);
        bool DeleteOrder(Order order);
        IEnumerable<Order> GetAllOrder();
        int SearchOrdersCount(string searchTerm, DateTime? searchDate);
        bool SaveOrder(Order order);
        IEnumerable<Statistical> RevenueStatistical(string option, int monthSearch, int yearSearch);
        IEnumerable<Order>  SearchOrdersNotConfirm(string searchTerm, DateTime? searchDate, int page, int recordSize);
        int SearchOrdersCountNotConfirm(string searchTerm, DateTime? searchDate);
    }
}
