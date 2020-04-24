using System;
using ApiProyect.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiProyect.Context
{
    public class PizzaShopDbContext : DbContext
    {
        public PizzaShopDbContext(DbContextOptions<PizzaShopDbContext> options)
            : base(options)
        {
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
