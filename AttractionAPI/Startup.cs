using AttractionAPI.Entities;
using AttractionAPI.Middleware;
using AttractionAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AttractionAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<AttractionDbContext>();
            services.AddScoped<AttractionSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IAttractionService, AttractionService>();
            services.AddScoped<ErrorHandlingMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AttractionSeeder seeder)
        {
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

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
