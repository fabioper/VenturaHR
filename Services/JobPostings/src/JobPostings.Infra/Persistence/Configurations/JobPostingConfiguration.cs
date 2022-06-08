using JobPostings.Domain.Aggregates.JobPostings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Persistence.Configurations;

public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.ToTable("JobPostings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.ExpireAt).IsRequired();
        builder.Property(x => x.Average).IsRequired();

        builder.HasOne(x => x.Company)
               .WithMany()
               .HasForeignKey("_companyId")
               .IsRequired();

        builder.OwnsOne(x => x.Salary,
            x => x.Property(c => c.Value).HasColumnName("Compensation"));

        builder.HasMany(x => x.Criterias)
               .WithOne()
               .HasForeignKey("_jobPostingId")
               .IsRequired();
    }
}