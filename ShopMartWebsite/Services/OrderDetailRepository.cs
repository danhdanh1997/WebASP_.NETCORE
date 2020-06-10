using Microsoft.EntityFrameworkCore;
using ShopMartWebsite.Data;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Services
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private ShopDbContext _ctx;

        public OrderDetailRepository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<OrderDetail> GetAllOrderDetailsByOrderId(int orderId)
        {
            return _ctx.orderDetails.Include(pr=>pr.product.category).Where(x => x.orderId == orderId).ToList();
        }
    }
}
