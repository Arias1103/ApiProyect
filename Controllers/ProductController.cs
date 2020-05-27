using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiProyect.Context;
using ApiProyect.DTOs;
using ApiProyect.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProyect.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PizzaShopDbContext context;
        private readonly IMapper mapper;

        public ProductController(PizzaShopDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var entities = await context.Products.ToListAsync();
            var dtos = mapper.Map<List<ProductDTO>>(entities);
            return dtos;
        }

        [HttpGet("{id}", Name = "GetProduct")]

        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var entity = await context.Products.Include(x => x.Branches).FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            return mapper.Map<ProductDTO>(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]CreationProductDTO creationProductDTO)
        {
            var entity = mapper.Map<Product>(creationProductDTO);
            context.Add(entity);
            using (var stream = new MemoryStream())
            {
                await creationProductDTO.Image.CopyToAsync(stream);
                entity.Image = Convert.ToBase64String(stream.ToArray());
            }
            await context.SaveChangesAsync();
            var dto = mapper.Map<ProductDTO>(entity);
            return new CreatedAtRouteResult("GetProduct", new { id= entity.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] CreationProductDTO creationProductDTO)
        {
            var entity = mapper.Map<Product>(creationProductDTO);
            entity.Id = id;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Products.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Product() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
