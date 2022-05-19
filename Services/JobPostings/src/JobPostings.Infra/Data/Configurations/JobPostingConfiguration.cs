using JobPostings.Domain.JobPostingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Data.Configurations;

public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.ToTable("JobPostings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired();

        builder.HasOne(x => x.Company)
               .WithMany()
               .HasForeignKey("_companyId")
               .HasPrincipalKey(x => x.ExternalId);

        builder.OwnsOne(x => x.Role,
            x => x.Property(r => r.Title).HasColumnName("Role"));

        builder.OwnsOne(x => x.Location,
            x => x.Property(l => l.Place).HasColumnName("Location"));

        builder.OwnsOne(x => x.Salary,
            x => x.Property(c => c.Value).HasColumnName("Compensation"));

        builder.OwnsOne(x => x.ExpireAt,
            x => x.Property(e => e.Date).HasColumnName("ExpireAt"));
    }
}