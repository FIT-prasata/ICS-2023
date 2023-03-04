using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TimeTracker.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimeTrackerDbContext>
{
    public TimeTrackerDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<TimeTrackerDbContext> builder = new();

        // // Use for LocalDB migrations
        // builder.UseSqlServer(
        //     @"Data Source=(LocalDB)\MSSQLLocalDB;
        //         Initial Catalog = CookBook;
        //         MultipleActiveResultSets = True;
        //         Integrated Security = True;
        //         Encrypt = False;
        //         TrustServerCertificate = True;");

        builder.UseSqlite($"Data Source=TimeTrackerDB;Cache=Shared");

        return new TimeTrackerDbContext(builder.Options);
    }
}