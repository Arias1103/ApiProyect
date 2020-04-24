using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProyect.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ApiProyect
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // other service configurations go here
            // replace "YourDbContext" with the class name of your DbContext
            services.AddDbContextPool<PizzaShopDbContext>(options => options
               // replace with your connection string
               .UseMySql("server=localhost;database=ApiPizzaShop;user=root;password=AIBL343190", mySqlOptions => mySqlOptions
                   // replace with your Server Version and Type
                   .ServerVersion(new Version(8, 0, 19), ServerType.MySql)
                  ));
            services.AddControllers().AddNewtonsoftJson(Options =>
            Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
