using Newtonsoft.Json;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task PlaceOrder_ReturnsOK()
        {
            var order = new OrderDto()
            {
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                TotalPrice = 123,
                UserId = "c30936a4-80b0-4209-84e2-98e9336e9c80",
                OrderRowsDto = new List<OrderRowDto>()
            };
            using (var client = new TestClientProvider().Client)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"/api/order/placeorder");

                var itemJson = JsonConvert.SerializeObject(order);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task ShowOrder_ReturnsOK()
        {
            // create order
            var order = new OrderDto()
            {
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                TotalPrice = 123,
                UserId = "c30936a4-80b0-4209-84e2-98e9336e9c80",
                OrderRowsDto = new List<OrderRowDto>()
            };
            using (var client = new TestClientProvider().Client)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"/api/order/placeorder");

                var itemJson = JsonConvert.SerializeObject(order);
                request.Content = new StringContent(itemJson, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // show order
                var responseShow = await client.GetAsync("/api/order/showorder?id=" + "c30936a4-80b0-4209-84e2-98e9336e9c80");

                Assert.Equal(HttpStatusCode.OK, responseShow.StatusCode);

            }
        }
        [Fact]
        public async Task ShowOrder_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/order/showorder?id=" + "b30936a4-80b0-4209-84e2-98e9336e9c80");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
