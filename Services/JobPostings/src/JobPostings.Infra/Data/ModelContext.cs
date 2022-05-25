#nullable disable

using System.Reflection;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.JobPostings;
using Microsoft.EntityFrameworkCore;

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