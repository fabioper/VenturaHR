#nullable disable

using System.Reflection;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence;

public class ModelContext : DbContext
{
    public DbSet<JobPosting> JobPostings { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<JobApplication> Applications { get; set; }

    public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}