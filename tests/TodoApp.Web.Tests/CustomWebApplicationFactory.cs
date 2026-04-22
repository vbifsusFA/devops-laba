using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Web.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptors = services.Where(
                d => d.ServiceType.Namespace != null && 
                     (d.ServiceType.Namespace.StartsWith("Microsoft.EntityFrameworkCore") ||
                      d.ServiceType == typeof(ApplicationDbContext) ||
                      d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            // Add InMemory Database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
        });
    }
}
