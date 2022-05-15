#nullable disable

using System.Reflection;
using JobPostings.Domain.JobPostingAggregate;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Data;

public class ModelContext : DbContext
{
    public DbSet<JobPosting> JobPostings { get; set; }

    public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}