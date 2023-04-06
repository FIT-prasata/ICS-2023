using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL;

namespace TimeTracker.Common.Tests;

public class TimeTrackerTestingDbContext : TimeTrackerDbContext
{
    private readonly bool _seedTestingData;

    public TimeTrackerTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData: false)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            UserSeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
        }
    }
}