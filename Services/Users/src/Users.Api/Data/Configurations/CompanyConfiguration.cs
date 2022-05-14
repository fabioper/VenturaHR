using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Api.Models.Entities;

namespace Users.Api.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);
        builder.Property(x => x.Email);

        builder.OwnsOne(
            x => x.Registration,
            b => b.Property(ad => ad.Number).HasColumnName("Registration")
        );

        builder.OwnsOne(
            x => x.PhoneNumber,
            b => b.Property(x => x.Value).HasColumnName("PhoneNumber")
        );
    }
}