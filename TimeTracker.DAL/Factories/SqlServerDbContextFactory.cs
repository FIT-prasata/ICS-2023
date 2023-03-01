using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL.Factories;

public class SqlServerDbContextFactory : IDbContextFactory<TimeTrackerDbContext>
{
    private readonly string _connectionString;
    private readonly bool _seedDemoData;

    public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
    {
        _connectionString = connectionString;
        _seedDemoData = seedDemoData;
    }

    public TimeTrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TimeTrackerDbContext> builder = new();
        builder.UseSqlServer(_connectionString);

        ////Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        //builder.LogTo(System.Console.WriteLine);
        //builder.EnableSensitiveDataLogging();

        return new TimeTrackerDbContext(builder.Options, _seedDemoData);
    }
}