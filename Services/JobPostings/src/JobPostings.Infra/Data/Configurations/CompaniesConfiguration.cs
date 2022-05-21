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

        builder.Property(x => x.Id)
               .HasConversion(value => value.Id, v => new CompanyId(v));

        builder.Property(x => x.Name).IsRequired();
    }
}