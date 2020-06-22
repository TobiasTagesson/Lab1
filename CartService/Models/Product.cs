using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public Guid ItemId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
    }
}
