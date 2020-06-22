using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    //    public class OrderItem
    //    {
    //        public int Id { get; set; }
    //        public Guid ItemId { get; set; }
    //        public int Amount { get; set; }
    //    }
    //    public class Order
    //    {
    //        public int Id { get; set; }
    //        public string UserId { get; set; }
    //        public List<OrderItem> OrderItems { get; set; }
    //        public DateTime OrderDate { get; set; }

    //    }
    //}
    //public OrderDto()
    //{
    //    OrderRowsDto = new List<OrderRowDto>();
    //}

    //public List<OrderRowDto> OrderRowsDto { get; set; }
    //public DateTime Date { get; set; }
    //public string UserId { get; set; }
    //public decimal TotalPrice { get; set; }
    //    }

    public class OrderDto
    {
        public OrderDto()
        {
            OrderRowsDto = new List<OrderRowDto>();

        }
        
        public Guid Id { get; set; }
        //public Guid OrderId { get; set; }
        public List<OrderRowDto> OrderRowsDto { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }


    public class OrderRowDto
    {
        [Key]
        public int Id { get; set; }
        //public string UserId { get; set; }
        public Guid ItemId { get; set; }
        public int Amount { get; set; }
            //public DateTime OrderDate { get; set; }

    }
}
