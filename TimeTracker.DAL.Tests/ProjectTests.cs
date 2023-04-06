using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class ProjectTests : DbContextTestsBase
    {
        public ProjectTests(ITestOutputHelper output) : base(output)
        {
        }
        //[Fact]
        //public async Task AddNew_Project()
        //{
        //    //Arrange            
        //    UserEntity userEntity = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = "Igor",
        //        LastName = "Rogi",
        //        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
        //    };

        //    TimeTrackerDbContextSUT.Users.Add(userEntity);
        //    await TimeTrackerDbContextSUT.SaveChangesAsync();

        //    ProjectEntity entity = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        CreatedById = userEntity.Id,
        //        Name = "Proj",
        //    };

        //    //Act
        //    TimeTrackerDbContextSUT.Projects.Add(entity);
        //    await TimeTrackerDbContextSUT.SaveChangesAsync();

        //    //Assert
        //    await using var dbx = await DbContextFactory.CreateDbContextAsync();
        //    var actualEntities = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
        //    Assert.Equal((entity.Id,entity.CreatedById,entity.Name),(actualEntities.Id,actualEntities.CreatedById,actualEntities.Name));
        //}
    }
}
