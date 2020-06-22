using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Services
{
    public interface IProductService
    {
        Product GetById(Guid Id);
        public Task<IEnumerable<Product>> GetAll();
    }
}
