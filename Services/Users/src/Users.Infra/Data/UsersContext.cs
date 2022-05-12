#nullable disable

using Microsoft.EntityFrameworkCore;
using Users.Infra.Data.Models;

namespace Users.Infra.Data;

public class UsersContext : DbContext
{
    public DbSet<UserProfile> Users { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userBuilder = modelBuilder.Entity<UserProfile>();

        userBuilder.ToTable("Users");
        userBuilder.HasKey(x => x.Id);
        userBuilder.Property(x => x.Name);
        userBuilder.Property(x => x.Email);
        userBuilder.Property(x => x.ExternalId);
    }
}