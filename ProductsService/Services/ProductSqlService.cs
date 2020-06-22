using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services
{
    public class ProductSqlService : IProductService
    {

        private readonly ProductDbContext _context;
        public ProductSqlService(ProductDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetById(Guid Id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == Id);

            return product; 
        }
    }
}
