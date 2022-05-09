using JobPostings.Domain.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        var jobPostings = builder.Metadata.FindNavigation(nameof(Company.JobPostings));
        jobPostings?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}