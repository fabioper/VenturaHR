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

        builder.Property(x => x.Role).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.OwnsOne(x => x.Location, x =>
        {
            x.Property(l => l.Place).HasColumnName("Location");
        });

        builder.OwnsOne(x => x.Salary, x =>
        {
            x.Property(c => c.Value).HasColumnName("Compensation");
        });

        builder.OwnsOne(x => x.ExpireAt, x =>
        {
            x.Property(e => e.Date).HasColumnName("ExpireAt");
        });

        builder.HasOne(x => x.Company)
               .WithMany(x => x.JobPostings)
               .HasForeignKey("_companyId")
               .IsRequired();
    }
}