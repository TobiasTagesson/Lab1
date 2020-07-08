using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTests : IClassFixture<ProductFixture>

    {
        ProductFixture _fixture;

        public ControllerTests(ProductFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllProducts_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products/getproducts");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products/getbyid?id=" + Guid.Empty);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            using (var client = new TestClientProvider().Client)
            {
                var productResponse = await client.GetAsync($"/api/products/getbyid?id={_fixture.product.Id}");

                using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.product.Id, product.Id);
                }
            }
        }

        [Fact]
        public async Task CreateProduct_Returns_CreatedProduct()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid productId = Guid.Empty;
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
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    productId = product.Id;

                    Assert.NotNull(product);
                    Assert.NotEqual<Guid>(Guid.Empty, productId);
                }

                var deleteResponse = await client.DeleteAsync($"/api/products/delete?id={productId}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }

        [Fact]
        public async Task DeleteProduct_ReturnsDeleted_Id()
        {
            using (var client = new TestClientProvider().Client)
            {
                Guid productId = Guid.Empty;

                var payload = JsonSerializer.Serialize(
                    new Product()
                    {
                        Description = "Testproduktbeskrivning",
                        Name = "Testprodukt",
                        Price = 123.45M,
                        ImageUrl = "https://shop.wilson.com/media/catalog/product/cache/38/image/9df78eab33525d08d6e5fb8d27136e95/c/f/cf893c83c0ff231061c2beb3f5a68306228e5d5c_wrt73900u_pro_staff_97_bl_bl_side.jpg"
                    });

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var createResponse = await client.PostAsync($"/api/products/create", content);

                using (var responseStream= await createResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    productId = product.Id;
                }

                var deleteResponse = await client.DeleteAsync($"/api/products/delete?id={productId}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(productId, deletedId);
                }
            }
        }
    }
}
