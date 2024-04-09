using IdentityServerService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IdentityServerService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [Authorize(Policy = "Read")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>() {
                new Product { Id = 1, Name = "Seat", Price = 100, Stock = 500 } ,
                     new Product { Id = 2, Name = "Chair", Price = 100, Stock = 500 },
                          new Product { Id = 3, Name = "Table", Price = 100, Stock = 500 },
                               new Product { Id = 4, Name = "Swing", Price = 100, Stock = 500 },
                                      new Product { Id = 5, Name = "Hammock", Price = 100, Stock = 500 }
            };

            return Ok(productList);
        }
        [Authorize(Policy = "CreateOrUpdate")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"id'si {id} olan product güncellenmiştir");
        }

        [Authorize(Policy = "CreateOrUpdate")]
        public IActionResult CreateProduct(Product product)
        {
            return Ok(product);
        }
    }
}
