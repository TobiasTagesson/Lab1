using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Services;
using Lab1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController (IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }


        public IActionResult PlaceOrder()
        {
            return View();
        }

        internal async Task<IEnumerable<OrderDto>> GetOrder()
        {
            var user = await _userManager.GetUserAsync(User);

            OrderViewModel vm = new OrderViewModel();

            var httpClient = new HttpClient();
            var order = await httpClient.GetStringAsync("https://localhost:44322/api/order/showorder?id=" + (user.Id));
            var orderItem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderDto>>(order);

            return orderItem;

        }

        public async Task<IActionResult> ViewOrder()
        {
            var orderItems = await GetOrder();
            if (orderItems == null)
                return NotFound("Order not found");

            ////var orderItems2 = orderItems.OrderBy(x => x.Date).FirstOrDefault();

            //List<OrderRow> orderRows = new List<OrderRow>();
            //var vm = new OrderViewModel();

            //foreach (var item in orderItems)
            //{
            //    foreach (var a in item.OrderRowsDto)
            //    {
            //        var orderItem = new OrderRow();
            //        orderItem.Product = await GetProductById(a.ItemId);
            //        orderItem.Amount = a.Amount;
            //        orderRows.Add(orderItem);
            //    }

            //    vm.Order.Date = item.Date;
            //    vm.Order.TotalPrice = item.TotalPrice;
            //    vm.Order.OrderRows = orderRows;
            //    vm.Order.UserId = item.UserId;
            //    vm.Order.Id = item.Id;
            //    var user = _userManager.GetUserAsync(User);
            //    vm.User = await user;
            //}



            // Selecting the order with the newest date
            var orderItems2 = orderItems.OrderByDescending(x => x.Date).FirstOrDefault();

            List<OrderRow> orderRows = new List<OrderRow>();
            var vm = new OrderViewModel();

            
                foreach (var a in orderItems2.OrderRowsDto)
                {
                var orderItem = new OrderRow();
                    orderItem.Product = await GetProductById(a.ItemId);
                    orderItem.Amount = a.Amount;
                    orderRows.Add(orderItem);
                }

            vm.Order.Date = orderItems2.Date;
                vm.Order.TotalPrice = orderItems2.TotalPrice;
                vm.Order.OrderRows = orderRows;
                vm.Order.UserId = orderItems2.UserId;
               vm.Order.Id = orderItems2.Id;
                var user = _userManager.GetUserAsync(User);
                vm.User = await user;
            

            return View("ViewOrder", vm);
        }


        public async Task<Product> GetProductById(Guid id)
        {
            var httpClient = new HttpClient();
            var cart = await httpClient.GetStringAsync("https://localhost:44350/api/products/GetById?id=" + (id));
            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(cart);

            return product;
        }
    }
}