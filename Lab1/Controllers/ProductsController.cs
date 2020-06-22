using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lab1.Controllers
{
   // [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


            List<Product> productList = new List<Product>();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44350/api/products/getproducts/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(productList);
        }


        //public IActionResult Index()
        //{
        //    var products = _productService.GetAll();

        //    return View(products);
        //}



        //[Authorize]
        //public IActionResult AddToCart(Guid id)
        //{
        //    var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
        //    string cartContent = "";

        //    if (cart.Value != null)
        //    {
        //        cartContent = cart.Value;
        //        cartContent += "," + id;
        //    }
        //    else
        //    {
        //        cartContent += id;
        //    }

        //    Response.Cookies.Append("cart", cartContent);

        //    return RedirectToAction("Index");
        //}


        [Authorize]
     //   [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var user = await _userManager.GetUserAsync(User);
            using (var httpClient = new HttpClient())
            {
                var item = new CartItemDto
                {
                    ItemId = productId,
                    UserId = user.Id
                };
               

                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:44315/cart/cart/addtocart");

                var itemJson = JsonConvert.SerializeObject(item);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                //request.Headers.Add("User-Agent", "AvcPgm.UI");
                var response = await httpClient.SendAsync(request);

                return RedirectToAction("Index");

            }


        }
    }
}
