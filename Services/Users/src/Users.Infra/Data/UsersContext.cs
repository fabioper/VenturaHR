#nullable disable

using Microsoft.EntityFrameworkCore;
using Users.Infra.Data.Models;
using Users.Infra.Data.Models.Entities;

namespace Users.Infra.Data;

public class UsersContext : DbContext
{
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Company> Companies { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureApplicants(modelBuilder);
        ConfigureCompanies(modelBuilder);
    }

    private static void ConfigureApplicants(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Applicant>();

        builder.ToTable("Applicants");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.Email);
        builder.OwnsOne(x => x.ExternalId, b =>
        {
            b.Property(x => x.Value).HasColumnName("ExternalId");
        });
    }

    private static void ConfigureCompanies(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Company>();
        builder.ToTable("Companies");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.Email);

        builder.OwnsOne(x => x.ExternalId, b =>
        {
            b.Property(x => x.Value).HasColumnName("ExternalId");
        });

        builder.OwnsOne(x => x.Registration, b =>
            b.Property(ad => ad.Number).HasColumnName("Registration"));

        builder.OwnsOne(x => x.PhoneNumber, b =>
            b.Property(x => x.Value).HasColumnName("PhoneNumber"));
    }
}