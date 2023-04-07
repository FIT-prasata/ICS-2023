using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using TimeTracker.DAL.Mappers;

namespace TimeTracker.DAL.Tests
{
    public class UserTests: DbContextTestsBase
    {
        public UserTests(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public async Task AddNewUser()
        {
            //Arrange
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };

            //Act
            TimeTrackerDbContextSUT.Users.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal((entity.Id, entity.FirstName, entity.LastName, entity.ImgUri), (actualEntities.Id, actualEntities.FirstName, actualEntities.LastName, actualEntities.ImgUri));
        }

        [Fact]
        public async Task GetAllUsers_ContainsSeededUser()
        {
            //Act
            var entities = await TimeTrackerDbContextSUT.Users.ToArrayAsync();

            //Assert
            Assert.Contains(UserSeeds.UserGet, entities);
        }

        [Fact]
        public async Task GetUserById()
        {
            //Act
            var entity = await TimeTrackerDbContextSUT.Users.FindAsync(UserSeeds.UserGet.Id);
            //Assert
            Assert.Equal(UserSeeds.UserGet, entity);
        }

        [Fact]
        public async Task UpdateUser()
        {
            //Arrange
            var baseEntity = UserSeeds.UserUpdate;
            var entity =
                baseEntity with
                {
                    FirstName = baseEntity + "Updated",
                    LastName = baseEntity + "Updated",
                };

            //Act
            TimeTrackerDbContextSUT.Users.Update(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task DeleteUser()
        {
            //Arrange
            var entityBase = UserSeeds.UserDelete;

            //Act
            TimeTrackerDbContextSUT.Users.Remove(entityBase);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TimeTrackerDbContextSUT.Users.AnyAsync(i => i.Id == entityBase.Id));
        }

        [Fact]
        public async Task DeleteUserById()
        {
            //Arrange
            var entityBase = UserSeeds.UserDelete;

            //Act
            TimeTrackerDbContextSUT.Remove(
                TimeTrackerDbContextSUT.Users.Single(i => i.Id == entityBase.Id));
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TimeTrackerDbContextSUT.Users.AnyAsync(i => i.Id == entityBase.Id));
        }

        [Fact]
        public async Task UserRepositoryExists()
        {
            //Arrange
            var repo = new Repository<UserEntity>(TimeTrackerDbContextSUT, new UserEntityMapper());
            UserEntity entity = UserSeeds.UserGet;

            //Assert
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task UserRepositoryInsert()
        {
            //Arrange
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            var repo = new Repository<UserEntity>(TimeTrackerDbContextSUT, new UserEntityMapper());

            Assert.False(await repo.ExistsAsync(entity));

            //Act
            await repo.InsertAsync(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task UserRepositoryUpdate()
        {
            //Arrange
            UserEntity entity = UserSeeds.UserUpdate;
            entity.FirstName = entity.FirstName + "Updated";

            var repo = new Repository<UserEntity>(TimeTrackerDbContextSUT, new UserEntityMapper());

            Assert.True(await repo.ExistsAsync(entity));

            //Act
            await repo.UpdateAsync(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            var entityU = repo.Get().Where(i => i.Id == entity.Id).ToList().First();
            //Assert
            Assert.Equal(entity, entityU);
        }
        [Fact]
        public async Task UserRepositoryDelete()
        {
            //Arrange
            UserEntity entity = UserSeeds.UserDelete;

            var repo = new Repository<UserEntity>(TimeTrackerDbContextSUT, new UserEntityMapper());

            Assert.True(await repo.ExistsAsync(entity));

            //Act
            repo.Delete(entity.Id);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.False(await repo.ExistsAsync(entity));
        }


        //TODO: add test for deleting user with all his own projects and its activities
    }
}