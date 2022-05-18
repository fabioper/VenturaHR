using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Api.Models.Entities;

namespace Users.Api.Data.Configurations;

public class CompanyConfiguration : BaseUserConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.OwnsOne(x => x.Registration,
            b => b.Property(ad => ad.Number).HasColumnName("Registration"));

        builder.OwnsOne(x => x.PhoneNumber,
            b => b.Property(x => x.Value).HasColumnName("PhoneNumber"));
    }
}