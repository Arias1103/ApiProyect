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
    public class BranchController : ControllerBase
    {
        private readonly PizzaShopDbContext context;

        public BranchController(PizzaShopDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Branch>> Get()
        {
            return context.Branches.Include(x => x.Products).ToList();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Branch>> Get(int id)
        {
            var branch = await context.Branches.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Branch branches)
        {
            context.Branches.Add(branches);
            context.SaveChanges();
            return new CreatedAtRouteResult(new { id = branches.Id }, branches);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Branch value)
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
        public ActionResult<Branch> Delete(int id)
        {
            var branch = context.Branches.FirstOrDefault(x => x.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            context.Branches.Remove(branch);
            context.SaveChanges();
            return branch;
        }
    }
}
