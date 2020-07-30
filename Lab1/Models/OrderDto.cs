using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    
    public class OrderDto
    {
        public OrderDto()
        {
            OrderRowsDto = new List<OrderRowDto>();
        }

        public Guid Id { get; set; }
        public List<OrderRowDto> OrderRowsDto { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderRowDto
    {


        public Guid ItemId { get; set; }
        public int Amount { get; set; }


    }
}
