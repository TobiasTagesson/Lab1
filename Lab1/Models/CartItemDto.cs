using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class CartItemDto
    {
            public Guid ItemId { get; set; }
            public int Amount { get; set; }
            public string UserId { get; set; }
    }
}
