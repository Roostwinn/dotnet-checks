using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This endpoint is accessible from any origin due to wildcard CORS policy");
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure CORS with wildcard origin
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    // ruleid: servicebuilder-wildcard-origin
                    builder.AllowAnyOrigin()  // This is the problematic wildcard origin
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors();  // Enable CORS middleware
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    // Example using WebApplication builder pattern
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure CORS with wildcard origin
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    // ruleid: servicebuilder-wildcard-origin
                    builder.AllowAnyOrigin()  // This is the problematic wildcard origin
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();
            app.UseCors();  // Enable CORS middleware
            app.MapControllers();

            app.Run();
        }
    }

    // Example using IApplicationBuilder directly
    public class DirectBuilderStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            // Configure CORS with wildcard origin directly in middleware pipeline
            app.UseCors(builder =>
            {
                // ruleid: servicebuilder-wildcard-origin
                builder.AllowAnyOrigin()  // This is the problematic wildcard origin
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
