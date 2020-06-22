﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Models;
using ProductsService.Services;

namespace ProductsService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
            var products = _service.GetAll();
            return Ok(products);
        }

        [HttpGet]
        public ActionResult<Product> GetById(Guid id)
        {
            var product = _service.GetById(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

    }
}