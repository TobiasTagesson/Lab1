using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Services
{
    public class MockProductService : IProductService
    {
        private List<Product> products = new List<Product>();

        public MockProductService()
        {

            for (int i = 0; i < 10; i++)
            {

                 Product product = new Product
                 {
                     Id = Guid.NewGuid(),
                     Name = "Wilson Pro Staff 97",
                     Description = "Racquet for the advanced player",
                     Price = 199.00M,
                     ImageUrl = "https://shop.wilson.com/media/catalog/product/cache/38/image/9df78eab33525d08d6e5fb8d27136e95/c/f/cf893c83c0ff231061c2beb3f5a68306228e5d5c_wrt73900u_pro_staff_97_bl_bl_side.jpg"

            };
            products.Add(product);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product GetById(Guid id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            return product;
        }
    }
}
