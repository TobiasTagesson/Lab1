using Lab1.Models;
using Lab1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lab1.Components
{
    public class CartSummary : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CartSummary(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetCartItems();
            var amount = CalculateCartAmount(items);
            var vm = new CartSummaryViewModel()
            {
                CartAmount = amount
            };
            return View(vm);
        }

        private async Task<IEnumerable<CartItemDto>> GetCartItems()
        {
            var Id = _userManager.GetUserId(Request.HttpContext.User);

            if (Id != null)
            {
            var httpClient = new HttpClient();
            var cart = await httpClient.GetStringAsync("https://localhost:44315/cart/cart/GetAllCartItemsByUserId/" + (Id));
            var cartItem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CartItemDto>>(cart);

            return cartItem;
            }
            else
            {
                return new List<CartItemDto>();
            }
        }

        private int CalculateCartAmount(IEnumerable<CartItemDto> cartItems)
        {
            var amount = cartItems.Select(x => x.Amount).Sum();
            return amount;
        }
    }
}
