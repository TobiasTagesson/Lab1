using CartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Services
{
    public class CartSQLService : ICartService
    {
        private readonly CartDbContext _context;

        public CartSQLService(CartDbContext context)
        {
            _context = context;
        }


        public Product AddToCart(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
            
        }

        public void DeleteCart(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var itemsToBeRemoved = GetAllCartItemsByUserId(userId);
            _context.RemoveRange(itemsToBeRemoved);
            _context.SaveChanges();
        }


        public List<Product> GetAllCartItemsByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var cartItems = _context.Products
                .Where(x => x.UserId == userId)
                .ToList();

            return cartItems;
        }

        public Product GetItemFromCart(Product product)
        {
            if (product == null)
             {
                throw new ArgumentNullException(nameof(product));
             }
            var item = _context.Products
                .FirstOrDefault(x => x.ItemId == product.ItemId
                && x.UserId == product.UserId);
            return item;
        }

        public Product Update(Product productChanges)
        {
            var product = _context.Products.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return productChanges;
        }
    }
}
