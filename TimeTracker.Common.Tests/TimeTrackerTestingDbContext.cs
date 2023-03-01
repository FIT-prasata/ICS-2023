using Microsoft.EntityFrameworkCore;
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

        }
    }
}