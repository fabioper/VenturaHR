using System;
using JobPostings.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JobPostings.Api.Tests;

public class ApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(ModelContext));

            services.AddDbContext<ModelContext>(options =>
                options.UseSqlite("Filename=:memory:"));

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var provider = scope.ServiceProvider;
            var db = provider.GetRequiredService<ModelContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            // try
            // {
            //     DbSeed.SeedDatabase(db);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex);
            //     throw;
            // }
        });
    }
}