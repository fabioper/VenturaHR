using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Models.Entities;

namespace Users.Infra.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);

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