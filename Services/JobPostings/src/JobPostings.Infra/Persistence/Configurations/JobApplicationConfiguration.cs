using JobPostings.Domain.Aggregates.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Infra.Persistence.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Id, x => new ApplicationId(x));

        builder.HasOne(x => x.Applicant)
               .WithMany()
               .HasForeignKey("_applicantId");

        builder.HasOne(x => x.JobPosting)
               .WithMany()
               .HasForeignKey("_jobPostingId");

        builder.Property(x => x.ApplicationDate).IsRequired();
    }
}