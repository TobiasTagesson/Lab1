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

        public Product Create (Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }
        public bool Delete(Guid id)
        {
            try
            {
                var product = GetById(id);
                _context.Products.Remove(product);
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;

            }
        }
    }
}
