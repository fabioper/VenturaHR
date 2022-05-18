using JobPostings.Domain.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPostings.Infra.Data.Configurations;

public class CompaniesConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);
        builder.Property(x => x.ExternalId);
        builder.HasIndex(x => x.ExternalId).IsUnique();

        var jobNavigation = builder.Metadata.FindNavigation(nameof(Company.JobPostings));
        jobNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}