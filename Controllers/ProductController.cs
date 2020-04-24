using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProyect.Context;
using ApiProyect.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProyect.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PizzaShopDbContext context;

        public ProductController(PizzaShopDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return context.Products.Include(x => x.Branches).ToList();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await context.Products.Include(x => x.Branches).FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Product products)
        {
            context.Products.Add(products);
            context.SaveChanges();
            return new CreatedAtRouteResult(new { id = products.Id }, products);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            context.SaveChanges();
            return product;
        }

    }
}
