using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Constraints;

namespace WebApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureServices();

            var app = builder.Build();
            app.Configure();

            app.Run();
        }
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddRouting(options => options.ConstraintMap.Add("enum", typeof(EnumConstraint)));
        }
        public static void Configure(this WebApplication app)
        {
            var env = app.Services.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
                app.UseExceptionHandler("/error-development");
            else
                app.UseExceptionHandler("/error");

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
