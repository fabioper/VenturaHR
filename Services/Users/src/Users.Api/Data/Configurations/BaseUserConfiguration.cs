using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Api.Models.Entities;

namespace Users.Api.Data.Configurations;

public abstract class BaseUserConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseUser
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);
        builder.Property(x => x.Email);
        builder.Property(x => x.ExternalId)
               .IsRequired();

        builder.HasIndex(x => x.ExternalId).IsUnique();
    }
}