using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMartWebsite.Interfaces;
using ShopMartWebsite.Models;

namespace ShopMartWebsite.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public IActionResult Index(string searchTerm, DateTime? searchDate, int? page)
        {
            int recordSize = 4;
            page = page ?? 1;
            var model = new OrderListViewModel();
            model.searchDate = searchDate;
            model.Orders = _orderRepository.SearchOrders(searchTerm, searchDate, page.Value, recordSize);
            //tong so cot
            int totalRecords = _orderRepository.SearchOrdersCount(searchTerm, searchDate);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        public IActionResult Viewss(int ID, string customer, decimal total)
        {
            var model = new OrderDetailListViewModel();
            model.customer = customer;
            model.total = total;
            model.OrderDetails = _orderDetailRepository.GetAllOrderDetailsByOrderId(ID);
            return PartialView("_View", model);
        }
        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var model = new OrderViewModel();
            var order= _orderRepository.GetOrderById(ID);
            model.id = order.id;
            return PartialView("_Delete", model);
        }
        [HttpPost]
        public IActionResult Delete(OrderViewModel model)
        {
            JsonResult json;
            var result = false;
            var order = _orderRepository.GetOrderById(model.id);
            //xóa logic
            order.status = false;
            result = _orderRepository.DeleteOrder(order);

            if (result)
            {
                json = new JsonResult(new { Success = true });

            }
            else
            {
                json = new JsonResult(new { Success = false, Message = "Đơn hàng xóa không thành công!!!" });
            }

            return json;
        }
        public IActionResult OrderNotConfirm(string searchTerm, DateTime? searchDate, int? page)
        {
            int recordSize = 4;
            page = page ?? 1;
            var model = new OrderListViewModel();
            model.searchDate = searchDate;
            model.Orders = _orderRepository.SearchOrdersNotConfirm(searchTerm, searchDate, page.Value, recordSize);
            //tong so cot
            int totalRecords = _orderRepository.SearchOrdersCountNotConfirm(searchTerm, searchDate);
            model.Pager = new Pager(totalRecords, page, recordSize);
           
            return View(model);
        }
        public IActionResult ConfirmOrder(int ID)
        {
            var model = new OrderViewModel();
            var order = _orderRepository.GetOrderById(ID);
            model.id = order.id;
            return PartialView("_ConfirmOrder", model);
        }
        [HttpPost]
        public IActionResult ConfirmOrder(OrderViewModel model)
        {
            JsonResult json;
            var result = false;
            var order = _orderRepository.GetOrderById(model.id);
            //xóa logic
            order.confirm = true;
            result = _orderRepository.DeleteOrder(order);

            if (result)
            {
                json = new JsonResult(new { Success = true });

            }
            else
            {
                json = new JsonResult(new { Success = false, Message = "Đơn hàng duyệt không thành công!!!" });
            }

            return json;
            
        }
    }
}