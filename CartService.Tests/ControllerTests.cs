using CartService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CartService.Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task GetAllCartItemsByUserId_Returns_NotFound()
        {
                using (var client = new TestClientProvider().Client)
                {
                    var response = await client.GetAsync("/cart/cart/getallcartitemsbyuserid?id=" + string.Empty);

                    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                }
        }

        //[Fact]
        //public async Task GetAllCartItemsByUserId_Returns_OK()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var response = await client.GetAsync("/cart/cart/getallcartitemsbyuserid?id=" + "c30936a4-80b0-4209-84e2-98e9336e9c80");

        //        response.EnsureSuccessStatusCode();
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    }
        //}

        [Fact]
        public async Task AddToCart_ReturnsOK()
        {
            
            var cartItem = new Product()
            {
                ItemId = Guid.NewGuid(),
                UserId = "c30936a4-80b0-4209-84e2-98e9336e9c80",
                Amount = 1
            };
            using (var client = new TestClientProvider().Client)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"/cart/cart/addtocart");

                var itemJson = JsonConvert.SerializeObject(cartItem);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

        }

        [Fact]
        public async Task DeleteCart_ReturnsOK()
        {
            // add item to cart

            var cartItem = new Product()
            {
                ItemId = Guid.NewGuid(),
                UserId = "c30936a4-80b0-4209-84e2-98e9336e9c80",
                Amount = 1
            };
            using (var client = new TestClientProvider().Client)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"/cart/cart/addtocart");

                var itemJson = JsonConvert.SerializeObject(cartItem);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // delete item from cart
                var responseDeleteItem = await client.DeleteAsync("/cart/cart/deletecart?id=" + "c30936a4-80b0-4209-84e2-98e9336e9c80");
                responseDeleteItem.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, responseDeleteItem.StatusCode);
            }

        }
    }
}
