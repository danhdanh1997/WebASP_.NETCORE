using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMartWebsite.Entities;
using ShopMartWebsite.Interfaces;
using ShopMartWebsite.Models;

namespace ShopMartWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChartController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public ChartController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index(string option, int? monthSearch, int? yearSearch)
        {
            if (monthSearch.HasValue)
            {
                if (monthSearch.Value == 0)
                {
                    monthSearch = DateTime.Now.Month;
                }
            }
            else
            {
                monthSearch = DateTime.Now.Month;
            }
            if (yearSearch.HasValue)
            {
                if (yearSearch.Value == 0)
                {
                    yearSearch = DateTime.Now.Year;
                }
            }
            else
            {
                yearSearch = DateTime.Now.Year;
            }
            var model = new StatisticalViewModel();
            if (option == null || option == "dayinmonth")
            {
                option = "dayinmonth";
                //string date = DateTime.Now.ToString("dd/MM/yyyy");
                model.option = option;
                model.monthSearch = monthSearch.Value;
                model.yearSearch = yearSearch.Value;
                model.statisticals = _orderRepository.RevenueStatistical(option, monthSearch.Value, yearSearch.Value).AsEnumerable().ToList();




            }
            else
            {
                model.option = option;
                model.monthSearch = 0;
                model.yearSearch = yearSearch.Value;
                model.statisticals = _orderRepository.RevenueStatistical(option, monthSearch.Value, yearSearch.Value).AsEnumerable().ToList();
            }
            return View(model);
        }
        public IActionResult ColumnChart(string option, int? monthSearch, int? yearSearch)
        {
            List<Statistical> obj = new List<Statistical>();
            obj = _orderRepository.RevenueStatistical(option, monthSearch.Value, yearSearch.Value).AsEnumerable().ToList();
            return Json(obj);
        }
    }
}