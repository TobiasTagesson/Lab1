using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Services;
using Lab1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        internal async Task<IEnumerable<CartItemDto>> GetCartItems()
        {
            var user = await _userManager.GetUserAsync(User);

            var httpClient = new HttpClient();
            var cart = await httpClient.GetStringAsync("https://localhost:44315/cart/cart/GetAllCartItemsByUserId/" + (user.Id));
            var cartItem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CartItemDto>>(cart);

            return cartItem;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = await GetCartItems();
            if (cartItems == null || cartItems.Count() == 0)
            { 
                return NotFound("Shoppingcart not found");
            }

           
            var vm = new CartViewModel();

            foreach (var item in cartItems)
            {
                var cartItem = new CartItem();
                cartItem.Product = await GetProductById(item.ItemId);
                cartItem.Amount = item.Amount;
                vm.Products.Add(cartItem);
            }
               
                vm.TotalPrice = vm.Products
                    .Select(x => x.Product.Price * x.Amount)
                    .Sum();

            return View(vm);
        }

        public async Task<IActionResult> CompleteOrder(ApplicationUser user)
        {
            var user1 = await _userManager.FindByIdAsync(user.Id);
            user1.FirstName = user.FirstName;
            user1.LastName = user.LastName;
            user1.StreetAddress = user.StreetAddress;
            user1.PostalCode = user.PostalCode;
            user1.City = user.City;
            user1.PhoneNumber = user.PhoneNumber;


            await _userManager.UpdateAsync(user1);


            return RedirectToAction("Index");
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var httpClient = new HttpClient();
            var cart = await httpClient.GetStringAsync("https://localhost:44350/api/products/GetById?id=" + (id));
            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(cart);

            return product;
        }


        public async Task<ActionResult> PlaceOrder([Bind("TotalPrice,Products")]CartViewModel cart)
        {
            var user = await _userManager.GetUserAsync(User);

            
            
            OrderDto order = new OrderDto();
            OrderViewModel vm = new OrderViewModel();

            if (user.StreetAddress == null)
            {
                vm.User = user;
                return View("CompleteOrder", vm);
            }

            foreach (var item in cart.Products)
            {
                OrderRowDto orderRowObject = new OrderRowDto();
                    orderRowObject.ItemId = item.Product.Id;
                    orderRowObject.Amount = item.Amount;
                    order.OrderRowsDto.Add(orderRowObject);
                
            }
            order.Date = DateTime.Now;
            order.UserId = user.Id;
            order.TotalPrice = cart.TotalPrice;
            var response = AddOrderToDatabase(order);

            await DeleteCart();

            return View("OrderSuccess", vm);
        }

        private async Task<IActionResult> DeleteCart()
        {
            var user = await _userManager.GetUserAsync(User);


            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:44315/cart/cart/DeleteCart?id=" + (user.Id));


            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Delete shoppingcart items failed.");
            }

            return Ok();


        }

        public async Task<IActionResult> AddOrderToDatabase(OrderDto order)
        {

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:44322/api/order/placeorder/");

                var itemJson = JsonConvert.SerializeObject(order);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request);

                return Ok();
            }

        }
    }
}