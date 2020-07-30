using System.Text.Json;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace OrderService.Tests
{
    public class ControllerTests : IClassFixture<OrderFixture>
    {
        OrderFixture _fixture;

        public ControllerTests(OrderFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task PlaceOrder_ReturnsOK()
        {
            Guid orderId = Guid.NewGuid();

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


                // Delete order


                orderId = order.Id;
                var responseDeleteItem = await client.DeleteAsync($"/api/order/delete?id={orderId}");
                responseDeleteItem.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task ShowOrder_ReturnsOK()
        {
            Guid orderId = Guid.NewGuid();

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

                //Delete order
                orderId = order.Id;
                var responseDeleteItem = await client.DeleteAsync($"/api/order/delete?id={orderId}");
                responseDeleteItem.EnsureSuccessStatusCode();

            }

        }

        [Fact]
        public async Task ShowOrder_ReturnsOrder()
        {
            using (var client = new TestClientProvider().Client)
            {
                var orderResponse = await client.GetAsync($"/api/order/showorder?id={_fixture.order.UserId}");

                using (var responseStream = await orderResponse.Content.ReadAsStreamAsync())
                {

                    var order = await System.Text.Json.JsonSerializer.DeserializeAsync<List<OrderDto>>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.order.Id, order[0].Id);
                }
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
