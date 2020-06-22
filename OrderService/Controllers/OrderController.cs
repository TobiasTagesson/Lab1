using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }


        public ActionResult<OrderDto> PlaceOrder(OrderDto order)
        {

           // var itemJson = JsonConvert.DeserializeObject<OrderDto>(order);
            _orderService.PlaceOrder(order);
            return Ok();
        }
        public ActionResult<OrderDto> ShowOrder(string id)
        {
            var orderItems = _orderService.GetOrderItemsByUserId(id);

            return Ok(orderItems);
        }
    }
}