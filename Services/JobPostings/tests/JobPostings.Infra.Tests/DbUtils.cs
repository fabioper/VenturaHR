using JobPostings.Infra.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Tests;

public class DbUtils
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<ModelContext> _contextOptions;

    public DbUtils()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<ModelContext>()
                          .UseSqlite(_connection)
                          .Options;
    }

    public ModelContext Context => new(_contextOptions);

    public void Dispose()
    {
        _connection.Dispose();
    }
}