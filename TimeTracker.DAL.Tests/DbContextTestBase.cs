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

            CookBookDbContextSUT = DbContextFactory.CreateDbContext();
        }

        protected IDbContextFactory<TimeTrackerDbContext> DbContextFactory { get; }
        protected TimeTrackerDbContext CookBookDbContextSUT { get; }


        public async Task InitializeAsync()
        {
            await CookBookDbContextSUT.Database.EnsureDeletedAsync();
            await CookBookDbContextSUT.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await CookBookDbContextSUT.Database.EnsureDeletedAsync();
            await CookBookDbContextSUT.DisposeAsync();
        }
    }
