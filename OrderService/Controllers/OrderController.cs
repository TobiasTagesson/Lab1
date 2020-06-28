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
            _orderService.PlaceOrder(order);
            return Ok();
        }
        public ActionResult<OrderDto> ShowOrder(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var orderItems = _orderService.GetOrderItemsByUserId(id);

            if (orderItems.Count == 0 || orderItems == null)
            {
                return NotFound();
            }
            return Ok(orderItems);
        }
    }
}