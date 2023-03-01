﻿using System;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<TimeTrackerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public TimeTrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TimeTrackerDbContext> builder = new();

        ////May be helpful for ad-hoc testing, not drop in replacement, needs some more configuration.
        //builder.UseSqlite($"Data Source =:memory:;");
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        ////Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        //builder.EnableSensitiveDataLogging();
        //builder.LogTo(Console.WriteLine); 

        return new TimeTrackerDbContext(builder.Options, _seedTestingData);
    }
}