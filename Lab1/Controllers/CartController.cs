using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Services;
using Lab1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string[] cartIds = new string[0];
            
            if(cart.Value != null)
            {
                 cartIds = cart.Value.Split(',');
            }

            
            var products = _productService.GetAll();

            CartViewModel vm = new CartViewModel();
            vm.Products = new List<CartItem>();


            foreach (string id in cartIds)
            {
                var guid = Guid.Parse(id);

                if(vm.Products.Count > 0 && vm.Products.Any(p => p.Product?.Id == guid))
                {
                    int productIndex = vm.Products.FindIndex(p => p.Product.Id == guid);
                    vm.Products[productIndex].Amount += 1;
                }
                else
                {
                    var p = _productService.GetById(guid);

                    if(p != null)
                    {
                    vm.Products.Add(new CartItem() { Amount = 1, Product = p });
                    }
                }
            }
            vm.TotalPrice = vm.Products.Sum(x => x.Product.Price * x.Amount);

            return View(vm);

        }

        [HttpPost]
        public async Task <IActionResult> PlaceOrder([Bind("TotalPrice,Products")]CartViewModel cart)
        {

            OrderViewModel vm = new OrderViewModel();
            Order order = new Order();
            order.TotalPrice = cart.TotalPrice;
            order.Date = DateTime.Now;
            order.UserId = Guid.Parse(_userManager.GetUserId(User));

             order.OrderRows = cart.Products.Select(cartItem => new OrderRow(cartItem)).ToList();

            vm.Order = order;

            var user = await _userManager.GetUserAsync(User);

            vm.User = user;

            return View("OrderSuccess", vm);
        }


    }
}