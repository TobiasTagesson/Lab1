using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public IActionResult AddToCart(Guid id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string cartContent = "";

            if (cart.Value != null)
            {
                cartContent = cart.Value;
                cartContent += "," + id;
            }
            else
            {
                cartContent += id;
            }

            Response.Cookies.Append("cart", cartContent);


            return Ok();
        }
    }
}
