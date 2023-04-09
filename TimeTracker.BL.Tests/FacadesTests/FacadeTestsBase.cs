using TimeTracker.BL.Mappers;
using TimeTracker.Common.Tests;
using TimeTracker.Common.Tests.Factories;
using TimeTracker.DAL;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Xunit;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests.FacadesTests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ActivityEntityMapper = new ActivityEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        UserEntityMapper = new UserEntityMapper();
        ProjectUserEntityMapper = new ProjectUserEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        ProjectModelMapper = new ProjectModelMapper(new ActivityModelMapper(), new ProjectUserModelMapper());
        ProjectUserModelMapper = new ProjectUserModelMapper();
        UserModelMapper = new UserModelMapper();

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<TimeTrackerDbContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }
    protected ProjectUserEntityMapper ProjectUserEntityMapper { get; }

    protected ActivityModelMapper ActivityModelMapper { get; }
    protected ProjectModelMapper ProjectModelMapper { get; }
    protected ProjectUserModelMapper ProjectUserModelMapper { get; }
    protected UserModelMapper UserModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
