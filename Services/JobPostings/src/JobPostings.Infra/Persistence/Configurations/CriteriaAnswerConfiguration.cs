using JobPostings.Domain.Aggregates.JobApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Persistence.Configurations;

public class CriteriaAnswerConfiguration : IEntityTypeConfiguration<CriteriaAnswer>
{
    public void Configure(EntityTypeBuilder<CriteriaAnswer> builder)
    {
        builder.ToTable("CriteriaAnswers");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Value).IsRequired();

        builder.HasOne(x => x.Criteria)
               .WithMany()
               .HasForeignKey(x => x.CriteriaId)
               .IsRequired();
    }
}