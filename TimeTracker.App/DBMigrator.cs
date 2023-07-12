using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL;

namespace TimeTracker.App;
interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class NoneDbMigrator : IDbMigrator
{
    public void Migrate() { }
    public Task MigrateAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

public class SqliteDbMigrator : IDbMigrator
{
    private readonly IDbContextFactory<TimeTrackerDbContext> _dbContextFactory;

    public SqliteDbMigrator(IDbContextFactory<TimeTrackerDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using TimeTrackerDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var recreateData = false;
        if (recreateData)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        // Ensures that database is created applying the latest state
        // Application of migration later on may fail
        // If you want to use migrations, you should create database by calling  dbContext.Database.MigrateAsync(cancellationToken) instead
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
