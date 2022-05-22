using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Api.Models.Entities;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, value => new UserId(value));

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.UserType).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.OwnsOne(x => x.Registration,
            b => b.Property(ad => ad.Number).HasColumnName("Registration"));

        builder.OwnsOne(x => x.PhoneNumber,
            b => b.Property(x => x.Value).HasColumnName("PhoneNumber"));
    }
}