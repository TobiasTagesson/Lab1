using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTests
    {
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
    }
}
