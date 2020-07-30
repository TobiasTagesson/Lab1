using CartService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CartService.Tests
{
    public class ControllerTests : IClassFixture<CartFixture>
    {
        CartFixture _fixture;

        public ControllerTests(CartFixture fixture)
        {
            _fixture = fixture;
        }



        [Fact]
        public async Task GetAllCartItemsByUserId_Returns_NotFound()
        {
                using (var client = new TestClientProvider().Client)
                {
                    var response = await client.GetAsync("/cart/cart/getallcartitemsbyuserid?id=" + string.Empty);

                    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                }
        }

        [Fact]
        public async Task GetAllCartItemsByUserId_Returns_OK()
        {
           

            using (var client = new TestClientProvider().Client)
            {
                var cartResponse = await client.GetAsync($"/cart/cart/getallcartitemsbyuserid/{_fixture.product.UserId}");

                cartResponse.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, cartResponse.StatusCode);
            }
        }

        [Fact]
        public async Task AddToCart_ReturnsOK()
        {
            using (var client = new TestClientProvider().Client)
            {
                string userId = "";
               
                var cartItem = new Product()
                {
                    ItemId = Guid.NewGuid(),
                    UserId = "c30936a4-80b0-4209-84e2-98e9336e9c80",
                    Amount = 1
                };
                var request = new HttpRequestMessage(HttpMethod.Post, $"/cart/cart/addtocart");

                var itemJson = JsonConvert.SerializeObject(cartItem);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);


                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                userId = cartItem.UserId;
                var responseDeleteItem = await client.DeleteAsync($"/cart/cart/deletecart?id={userId}");
                responseDeleteItem.EnsureSuccessStatusCode();


            }

        }

        [Fact]
        public async Task DeleteCart_ReturnsOK()
        {
            // add item to cart
            Guid productId = Guid.Empty;
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

                productId = cartItem.ItemId;
                var responseDeleteItem = await client.DeleteAsync($"/cart/cart/deletecart?id={productId}");
                responseDeleteItem.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, responseDeleteItem.StatusCode);
            }

        }
    }
}
