using Lab1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class Order
    {
        public Order()
        {
            OrderRows = new List<OrderRow>();
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public List<OrderRow> OrderRows { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderRow
    {
        public OrderRow() { }
        public OrderRow(CartItem cartItem)
        {
            Amount = cartItem.Amount;
            Product = cartItem.Product;
        }

        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}

