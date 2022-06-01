using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Persistence.Configurations;

public class CriteriaAnswerConfiguration : IEntityTypeConfiguration<CriteriaAnswer>
{
    public void Configure(EntityTypeBuilder<CriteriaAnswer> builder)
    {
        builder.ToTable("CriteriaAnswers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(x => x.Id, x => new CriteriaAnswerId(x));

        builder.Property(x => x.Value).IsRequired();

        builder.HasOne(x => x.Criteria)
               .WithMany()
               .HasForeignKey(x => x.CriteriaId)
               .IsRequired();
    }
}