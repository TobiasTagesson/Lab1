using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class OrderSqlService : IOrderService
    {

        private readonly OrderDbContext _context;

        public OrderSqlService(OrderDbContext context)
        {
            _context = context;
        }

        public List<OrderDto> GetOrderItemsByUserId(string id)
        {
            if (String.IsNullOrEmpty(id))
                
                throw new ArgumentNullException(nameof(id));
 
            // Krasch ibland?
            var orderItems = _context.Orders.Include(z => z.OrderRowsDto)
                .Where(x => x.UserId == id)
                .ToList();


            return orderItems;
        }

        public OrderDto PlaceOrder(OrderDto order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public bool Delete(Guid id)
        {
            try
            {
                OrderDto order = new OrderDto() { Id = id };
                _context.Orders.Attach(order);
                _context.Orders.Remove(order);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}
