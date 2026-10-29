using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Временная база данных в памяти (для простоты лабы)
        private static readonly List<Product> Products = new()
        {
            new Product { Id = 1, Name = "Ноутбук", Description = "Мощный игровой ноутбук", Price = 75000, Stock = 5 },
            new Product { Id = 2, Name = "Мышь", Description = "Беспроводная мышь", Price = 2500, Stock = 15 },
            new Product { Id = 3, Name = "Клавиатура", Description = "Механическая клавиатура", Price = 5000, Stock = 8 }
        };

        // GET: api/products (Захар будет вызывать этот метод для каталога)
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(Products);
        }

        // POST: api/products (Для добавления нового товара)
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            product.Id = Products.Max(p => p.Id) + 1;
            Products.Add(product);
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }
    }
}
