using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.Repositories;
using TimeTracker.DAL.Seeds;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class ActivityTests : DbContextTestsBase
    {
        public ActivityTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNewActivity()
        {
            //Arrange
            ActivityEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = UserSeeds.UserEntity1.Id,
                ProjectId = ProjectSeeds.ProjectEntity1.Id
            };

            //Act
            TimeTrackerDbContextSUT.Activities.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities);
        }

        [Fact]
        public async Task GetAllActivities_ContainsSeededActivity()
        {
            //Act
            var entities = await TimeTrackerDbContextSUT.Activities.ToArrayAsync();

            //Assert
            Assert.Contains(ActivitySeeds.ActivityGet, entities);
        }

        [Fact]
        public async Task GetActivityById()
        {
            //Act
            var entity = await TimeTrackerDbContextSUT.Activities.FindAsync(ActivitySeeds.ActivityGet.Id);
            //Assert
            Assert.Equal(ActivitySeeds.ActivityGet, entity);
        }

        [Fact]
        public async Task UpdateActivity()
        {
            //Arrange
            var baseEntity = ActivitySeeds.ActivityUpdate;
            var entity =
                baseEntity with
                {
                    Description = baseEntity + "Updated",
                    Type = Enums.ActivityType.Break,
                    Start = DateTime.Parse("2023-12-31 23:59:59"),
                    End = DateTime.Parse("2023-12-31 23:59:59")
                };

            //Act
            TimeTrackerDbContextSUT.Activities.Update(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task DeleteActivity()
        {
            //Arrange
            var entityBase = ActivitySeeds.ActivityDelete;

            //Act
            TimeTrackerDbContextSUT.Activities.Remove(entityBase);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TimeTrackerDbContextSUT.Activities.AnyAsync(i => i.Id == entityBase.Id));
        }

        [Fact]
        public async Task DeleteActivityById()
        {
            //Arrange
            var entityBase = ActivitySeeds.ActivityDelete;

            //Act
            TimeTrackerDbContextSUT.Remove(
                TimeTrackerDbContextSUT.Activities.Single(i => i.Id == entityBase.Id));
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TimeTrackerDbContextSUT.Activities.AnyAsync(i => i.Id == entityBase.Id));
        }
        [Fact]
        public async Task ActivityRepositoryExists()
        {
            //Arrange
            var repo = new Repository<ActivityEntity>(TimeTrackerDbContextSUT, new ActivityEntityMapper());
            var entity = ActivitySeeds.ActivityGet;

            //Assert
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task ActivityRepositoryInsert()
        {
            //Arrange
            ActivityEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = UserSeeds.UserEntity1.Id,
                ProjectId = ProjectSeeds.ProjectEntity1.Id
            };
            var repo = new Repository<ActivityEntity>(TimeTrackerDbContextSUT, new ActivityEntityMapper());

            Assert.False(await repo.ExistsAsync(entity));

            //Act
            await repo.InsertAsync(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task ActivityRepositoryUpdate()
        {
            //Arrange
            ActivityEntity entity = ActivitySeeds.ActivityUpdate;
            entity.Description = entity.Description + "Updated";

            var repo = new Repository<ActivityEntity>(TimeTrackerDbContextSUT, new ActivityEntityMapper());

            Assert.True(await repo.ExistsAsync(entity));

            //Act
            await repo.UpdateAsync(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            var entityU = repo.Get().Where(i => i.Id == entity.Id).ToList().First();
            //Assert
            Assert.Equal(entity, entityU);
        }
        [Fact]
        public async Task ActivityRepositoryDelete()
        {
            //Arrange
            ActivityEntity entity = ActivitySeeds.ActivityDelete;

            var repo = new Repository<ActivityEntity>(TimeTrackerDbContextSUT, new ActivityEntityMapper());

            Assert.True(await repo.ExistsAsync(entity));

            //Act
            repo.Delete(entity.Id);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.False(await repo.ExistsAsync(entity));
        }
    }
}
