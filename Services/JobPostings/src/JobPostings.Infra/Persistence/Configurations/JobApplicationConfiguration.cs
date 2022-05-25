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

        builder.Property(x => x.Id)
               .HasConversion(x => x.Id, x => new JobApplicationId(x));

        builder.HasOne(x => x.Applicant)
               .WithMany()
               .HasForeignKey("_applicantId");

        builder.HasOne(x => x.JobPosting)
               .WithMany()
               .HasForeignKey("_jobPostingId");

        builder.Property(x => x.ApplicationDate).IsRequired();
    }
}