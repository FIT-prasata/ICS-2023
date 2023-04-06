using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
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
            UserEntity userEntity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };

            ProjectEntity projectEntity = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                CreatedById = userEntity.Id
            };

            ActivityEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = userEntity.Id,
                ProjectId = projectEntity.Id
            };
            //Act
            TimeTrackerDbContextSUT.Users.Add(userEntity);
            TimeTrackerDbContextSUT.Projects.Add(projectEntity);
            TimeTrackerDbContextSUT.Activities.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal((entity.Id, entity.Start, entity.End, entity.Description, entity.Type, entity.CreatedById, entity.ProjectId), (actualEntities.Id, actualEntities.Start, actualEntities.End, actualEntities.Description, actualEntities.Type, actualEntities.CreatedById, actualEntities.ProjectId));
        }

        [Fact]
        public async Task AddNewActivity_With_Existing_Id()
        {
            //Arrange
            UserEntity userEntity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            ProjectEntity projectEntity = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                CreatedById = userEntity.Id
            };
            ActivityEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = userEntity.Id,
                ProjectId = projectEntity.Id
            };
            ActivityEntity entity2 = new()
            {
                Id = entity.Id,
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = userEntity.Id,
                ProjectId = projectEntity.Id
            };
            //Act
            TimeTrackerDbContextSUT.Users.Add(userEntity);
            TimeTrackerDbContextSUT.Projects.Add(projectEntity);
            TimeTrackerDbContextSUT.Activities.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            try
            {
                TimeTrackerDbContextSUT.Activities.Add(entity2);
                await TimeTrackerDbContextSUT.SaveChangesAsync();
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task DeleteExistingActivity()
        {
            //Arrange
            UserEntity userEntity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };

            ProjectEntity projectEntity = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                CreatedById = userEntity.Id
            };

            ActivityEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = userEntity.Id,
                ProjectId = projectEntity.Id
            };

            //Act
            TimeTrackerDbContextSUT.Users.Add(userEntity);
            TimeTrackerDbContextSUT.Projects.Add(projectEntity);
            TimeTrackerDbContextSUT.Activities.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            TimeTrackerDbContextSUT.Activities.Remove(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Activities.SingleOrDefaultAsync(i => i.Id == entity.Id);
            Assert.Null(actualEntities);
        }

        [Fact]
        public async Task DeleteNonExistingActivity()
        {
            //Arrange
            UserEntity userEntity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };

            ProjectEntity projectEntity = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                CreatedById = userEntity.Id
            };

            ActivityEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = userEntity.Id,
                ProjectId = projectEntity.Id
            };
            //Act
            TimeTrackerDbContextSUT.Users.Add(userEntity);
            TimeTrackerDbContextSUT.Projects.Add(projectEntity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            try
            {
                TimeTrackerDbContextSUT.Activities.Remove(entity);
                await TimeTrackerDbContextSUT.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }
            Assert.False(true);
        }

        //[Fact]
        //public async Task UpdateActivity()
        //{
        //    //Arrange
        //    UserEntity userEntity = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = "Igor",
        //        LastName = "Rogi",
        //        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
        //    };

        //    ProjectEntity projectEntity = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Test",
        //        Description = "Test",
        //        CreatedById = userEntity.Id
        //    };

        //    ActivityEntity entity = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        Start = DateTime.Now,
        //        End = DateTime.Now,
        //        Description = "Test",
        //        Type = Enums.ActivityType.Work,
        //        CreatedById = userEntity.Id,
        //        ProjectId = projectEntity.Id
        //    };

        //    //Act
        //    TimeTrackerDbContextSUT.Users.Add(userEntity);
        //    TimeTrackerDbContextSUT.Projects.Add(projectEntity);
        //    TimeTrackerDbContextSUT.Activities.Add(entity);
        //    await TimeTrackerDbContextSUT.SaveChangesAsync();

        //    entity.Start = DateTime.Now;
        //    entity.End = DateTime.Now;
        //    entity.Description = "Test2";
        //    entity.Type = Enums.ActivityType.Break;


        //    TimeTrackerDbContextSUT.Activities.Update(entity);
        //    await TimeTrackerDbContextSUT.SaveChangesAsync();

        //    //Assert
        //    await using var dbx = await DbContextFactory.CreateDbContextAsync();
        //    var actualEntities = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        //    Assert.Equal(entity, actualEntities);
        //}
    }
}
