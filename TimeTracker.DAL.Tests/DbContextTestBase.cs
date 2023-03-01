using System;
using System.Threading.Tasks;
using TimeTracker.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Tests;
using TimeTracker.Common.Tests.Factories;
using Xunit;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests;
    public class DbContextTestsBase : IAsyncLifetime
    {
        protected DbContextTestsBase(ITestOutputHelper output)
        {
            XUnitTestOutputConverter converter = new(output);
            Console.SetOut(converter);

            // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
            // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);

            DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

            TimeTrackerDbContextSUT = DbContextFactory.CreateDbContext();
        }

        protected IDbContextFactory<TimeTrackerDbContext> DbContextFactory { get; }
        protected TimeTrackerDbContext TimeTrackerDbContextSUT { get; }


        public async Task InitializeAsync()
        {
            await TimeTrackerDbContextSUT.Database.EnsureDeletedAsync();
            await TimeTrackerDbContextSUT.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await TimeTrackerDbContextSUT.Database.EnsureDeletedAsync();
            await TimeTrackerDbContextSUT.DisposeAsync();
        }
    }
