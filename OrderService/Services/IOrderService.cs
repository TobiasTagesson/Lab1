using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public interface IOrderService
    {
        OrderDto PlaceOrder(OrderDto order);
        List<OrderDto> GetOrderItemsByUserId(string id);
        bool Delete(Guid id);


    }
}
