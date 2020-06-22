using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Models;
using CartService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("cart/[controller]/[action]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<Product>> GetAllCartItemsByUserId(string userId)
        {
            var items = _cartService.GetAllCartItemsByUserId(userId);
            if (items.Count == 0)
            {
                return NotFound("The cart is empty");
            }
            else
            {

                return Ok(items);
            }
        }
        [HttpPost]
        public ActionResult<Product> AddToCart(Product product)
        {
            var itemTocart = new Product();

            var itemExist = _cartService.GetItemFromCart(product);

            if (itemExist == null)
            {
                product.Amount = 1;
                _cartService.AddToCart(product);
            }
            else
            {
                itemExist.Amount++;
                _cartService.Update(itemExist);
                

            }

                return Ok();
        }

        public ActionResult<Product> DeleteCart(string id)
        {
            _cartService.DeleteCart(id);

            return Ok();
        }
    }

}