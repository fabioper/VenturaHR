using System.Reflection;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JobPostings.Infra.Data;

public class ModelContext : DbContext
{
    public DbSet<JobPosting> JobPostings { get; set; }
    public DbSet<Company> Companies { get; set; }

    public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}