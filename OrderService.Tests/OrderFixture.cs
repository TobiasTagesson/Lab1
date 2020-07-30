using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Tests
{
    public class OrderFixture : IDisposable
    {
        public OrderDto order { get; private set; }

        public OrderFixture()
        {
            order = Initialize().Result;
        }

        private async Task<OrderDto> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new OrderDto()
                    {
                        Date = DateTime.Now,
                        Id = Guid.NewGuid(),
                        TotalPrice = 123,
                        UserId = "c30936a4-80b0-4209-84e2-98e9336e9c80",
                        OrderRowsDto = new List<OrderRowDto>()

                    }) ;

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/order/placeorder", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var createdOrder = await JsonSerializer.DeserializeAsync<OrderDto>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return createdOrder;
                }
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/order/delete?id={order.Id}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deleteId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
