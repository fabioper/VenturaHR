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

        builder.Property(x => x.Id)
               .HasConversion(x => x.Id, x => new JobPostingId(x));

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.HasOne(x => x.Company)
               .WithMany()
               .HasForeignKey("_companyId");

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