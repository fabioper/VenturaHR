using System.Linq;
using JobPostings.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobPostings.Api.Tests;

#nullable disable

public class ApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ModelContext>));

            services.Remove(descriptor);

            services.AddDbContext<ModelContext>(options =>
                options.UseSqlite("Filename=:memory:"));

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var provider = scope.ServiceProvider;
            var db = provider.GetRequiredService<ModelContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });
    }
}