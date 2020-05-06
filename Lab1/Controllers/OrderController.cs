using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult PlaceOrder()
        {
            return View();
        }
    }
}