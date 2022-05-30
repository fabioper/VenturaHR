using JobPostings.Domain.Aggregates.Application;
using JobPostings.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Persistence.Configurations;

public class CriteriaFullfillmentConfiguration : IEntityTypeConfiguration<CriteriaFullfillment>
{
    public void Configure(EntityTypeBuilder<CriteriaFullfillment> builder)
    {
        builder.ToTable("CriteriaFullfillments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(x => x.Id, x => new CriteriaFullfillmentId(x));

        builder.Property(x => x.Value).IsRequired();

        builder.HasOne(x => x.Criteria)
               .WithMany()
               .HasForeignKey("_criteriaId")
               .IsRequired();
    }
}