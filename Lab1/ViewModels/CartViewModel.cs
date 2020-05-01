using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
