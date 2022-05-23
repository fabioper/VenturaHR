#nullable disable

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Models.Entities;

namespace Users.Infra.Persistence;

public class UsersContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}