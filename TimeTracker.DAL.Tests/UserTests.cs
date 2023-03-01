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

    }
}