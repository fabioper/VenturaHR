using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Common;
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

        builder.Property(x => x.AppliedAt).IsRequired();

        builder.HasOne(x => x.Applicant)
               .WithMany()
               .HasForeignKey("_applicantId")
               .IsRequired();

        builder.HasOne(x => x.JobPosting)
               .WithMany()
               .HasForeignKey("_jobPostingId")
               .IsRequired();

        var criterias = builder.Metadata.FindNavigation(nameof(JobApplication.CriteriasAnswers));
        criterias?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.CriteriasAnswers)
               .WithOne()
               .HasForeignKey("_applicationId")
               .IsRequired();
    }
}