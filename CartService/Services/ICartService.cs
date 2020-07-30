using CartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Services
{
    public interface ICartService
    {
        Product AddToCart(Product product);

        void DeleteCart(string userId);
        public List<Product> GetAllCartItemsByUserId(string userId);
        Product GetItemFromCart(Product product);
        public Product Update(Product product);

    }
}
