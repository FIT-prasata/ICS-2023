using TimeTracker.DAL;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests.Factories;

public class DbContextSqLiteTestingFactory : IDbContextFactory<TimeTrackerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public TimeTrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TimeTrackerDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new TimeTrackerTestingDbContext(builder.Options, _seedTestingData);
    }
}