using CartService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartService.Tests
{
    public class CartFixture : IDisposable
    {
        public Product product { get; private set; }

        public CartFixture()
        {
            product = Initialize().Result;
        }

        private async Task<Product> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Product()
                    {
                        ItemId = new Guid ("a9e5806a-5529-442c-a5be-f557f9494f39"),
                        Amount = 1,
                        UserId = "eeb2cc01-0120-48d7-a7a7-9654301f6477"

                    });

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/cart/cart/addtocart", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var createdProduct = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return createdProduct;
                }
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/cart/cart/deletecart?id={product.UserId}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deleteId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
