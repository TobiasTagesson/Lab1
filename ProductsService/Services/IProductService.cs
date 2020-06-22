using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services
{
        public interface IProductService
        {
            Product GetById(Guid Id);
            IEnumerable<Product> GetAll();
        }
    }

