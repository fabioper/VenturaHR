using JobPostings.Domain.Aggregates.Applicants;
using JobPostings.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Persistence.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicants");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Id, x => new ApplicantId(x));

        builder.Property(x => x.Name).IsRequired();
    }
}