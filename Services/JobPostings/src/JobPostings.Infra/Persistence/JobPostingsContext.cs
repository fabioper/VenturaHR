#nullable disable

using System.Reflection;
using Common.Abstractions;
using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Aggregates.Companies;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Aggregates.JobPostings;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Persistence;

public class JobPostingsContext : DbContext
{
    public DbSet<JobPosting> JobPostings { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<JobApplication> Applications { get; set; }
    public DbSet<Criteria> Criterias { get; set; }
    public DbSet<CriteriaAnswer> CriteriaAnswers { get; set; }

    public JobPostingsContext(DbContextOptions<JobPostingsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = entry.Entity.CreatedAt;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}