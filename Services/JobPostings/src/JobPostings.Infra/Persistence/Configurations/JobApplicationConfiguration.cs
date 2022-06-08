using JobPostings.Domain.Aggregates.JobApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Persistence.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable("Applications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Average).IsRequired();

        builder.HasOne(x => x.Applicant)
               .WithMany()
               .HasForeignKey("_applicantId")
               .IsRequired();

        builder.HasOne(x => x.JobPosting)
               .WithMany()
               .HasForeignKey("_jobPostingId")
               .IsRequired();

        builder.HasMany(x => x.Answers)
               .WithOne()
               .HasForeignKey("_applicationId")
               .IsRequired();
    }
}