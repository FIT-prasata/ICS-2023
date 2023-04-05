using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class UserTests: DbContextTestsBase
    {
        public UserTests(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public async Task AddNew_User()
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
        public async Task AddNew_User_With_Existing_Id()
        {
            //Arrange
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };

            UserEntity entity2 = new()
            {
                Id = entity.Id,
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            //Act
            TimeTrackerDbContextSUT.Users.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            try
            {
                TimeTrackerDbContextSUT.Users.Add(entity2);
                await TimeTrackerDbContextSUT.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }
            Assert.True(false);
        }

        [Fact]
        public async Task DeleteExisting_User()
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
            TimeTrackerDbContextSUT.Users.Remove(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Users.SingleOrDefaultAsync(i => i.Id == entity.Id);
            Assert.Null(actualEntities);
        }

        [Fact]
        public async Task DeleteNonExisting_User()
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
            try
            {
                TimeTrackerDbContextSUT.Users.Remove(entity);
                await TimeTrackerDbContextSUT.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }
            Assert.False(true);
            //Assert
        }
    }
}