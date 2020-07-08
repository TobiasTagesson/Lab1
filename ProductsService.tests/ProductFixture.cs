using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductsService.Tests
{
    public class ProductFixture : IDisposable
    {
        public Product product { get; private set; }

        public ProductFixture()
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
                        Description = "Testproduktbeskrivning",
                        Name = "Testprodukt",
                        Price = 123.45M,
                        ImageUrl = "https://shop.wilson.com/media/catalog/product/cache/38/image/9df78eab33525d08d6e5fb8d27136e95/c/f/cf893c83c0ff231061c2beb3f5a68306228e5d5c_wrt73900u_pro_staff_97_bl_bl_side.jpg"
                    });

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/api/products/create", content);

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
                var deleteResponse = await client.DeleteAsync($"/api/products/delete?id={product.Id}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deleteId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }

    }
}
