using System;
using System.Collections.Generic;
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
    public class BranchController : ControllerBase
    {
        private readonly PizzaShopDbContext context;
        private readonly IMapper mapper;

        public BranchController(PizzaShopDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BranchDTO>>> Get()
        {
            var entities = await context.Branches.Include(x => x.Products).ToListAsync();
            var dtos = mapper.Map<List<BranchDTO>>(entities);
            return dtos;
        }

        [HttpGet("{id}", Name="GetBranch")]

        public async Task<ActionResult<BranchDTO>> Get(int id)
        {
            var entity = await context.Branches.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<BranchDTO>(entity);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreationBranchDTO creationBranchDTO)
        {
            var entity = mapper.Map<Branch>(creationBranchDTO);
            context.Add(entity);
            await context.SaveChangesAsync();
            var branchDTO = mapper.Map<BranchDTO>(entity);

            return new CreatedAtRouteResult("GetBranch", new { id = branchDTO.Id }, branchDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreationBranchDTO creationBranchDTO)
        {
            var entity = mapper.Map<Branch>(creationBranchDTO);
            entity.Id = id;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {
            var exist = await context.Branches.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Branch() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
