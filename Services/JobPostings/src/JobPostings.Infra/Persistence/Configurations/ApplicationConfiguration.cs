using JobPostings.Domain.Aggregates.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationId = JobPostings.Domain.Common.ApplicationId;

namespace JobPostings.Infra.Persistence.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Id, x => new ApplicationId(x));

        builder.Property(x => x.AppliedAt).IsRequired();

        builder.HasOne(x => x.Applicant)
               .WithMany()
               .HasForeignKey("_applicantId")
               .IsRequired();

        builder.HasOne(x => x.JobPosting)
               .WithMany()
               .HasForeignKey("_jobPostingId")
               .IsRequired();

        var criterias = builder.Metadata.FindNavigation(nameof(Application.CriteriasFullfillments));
        criterias?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.CriteriasFullfillments)
               .WithOne()
               .HasForeignKey("_applicationId")
               .IsRequired();
    }
}