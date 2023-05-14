using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.Seeds;
using Xunit.Abstractions;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.DAL.Tests
{
    public class UnitOfWorkTests : DbContextTestsBase
    {
        public UnitOfWorkTests(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public async Task UnitOfWorkInsertUser()
        {
            var workUnit = new UnitOfWork.UnitOfWork(TimeTrackerDbContextSUT);
            var repo = workUnit.GetRepository<UserEntity, UserEntityMapper>();
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            //Act
            await repo.InsertAsync(entity);
            await workUnit.CommitAsync();            
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task UnitOfWorkInsertProject()
        {
            var workUnit = new UnitOfWork.UnitOfWork(TimeTrackerDbContextSUT);
            var repo = workUnit.GetRepository<ProjectEntity, ProjectEntityMapper>();
            ProjectEntity entity = new()
            {
                Id = Guid.NewGuid(),
                CreatedById = UserSeeds.UserEntity1.Id,
                Name = "New Project 1",
            };
            //Act
            await repo.InsertAsync(entity);
            await workUnit.CommitAsync();
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task UnitOfWorkInsertActivity()
        {
            var workUnit = new UnitOfWork.UnitOfWork(TimeTrackerDbContextSUT);
            var repo = workUnit.GetRepository<ActivityEntity, ActivityEntityMapper>();
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
            await repo.InsertAsync(entity);
            await workUnit.CommitAsync();
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task UnitOfWorkBulkInsert()
        {
            var workUnit = new UnitOfWork.UnitOfWork(TimeTrackerDbContextSUT);
            var repoUser = workUnit.GetRepository<UserEntity, UserEntityMapper>();
            var repoProject = workUnit.GetRepository<ProjectEntity, ProjectEntityMapper>();
            var repoActivity = workUnit.GetRepository<ActivityEntity, ActivityEntityMapper>();
            UserEntity entityUser = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            ProjectEntity entityProject = new()
            {
                Id = Guid.NewGuid(),
                CreatedById = entityUser.Id,
                Name = "New Project 1",
            };
            ActivityEntity entityActivity = new()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Test",
                Type = Enums.ActivityType.Work,
                CreatedById = entityUser.Id,
                ProjectId = entityProject.Id,
            };
            //Act
            await repoActivity.InsertAsync(entityActivity);
            await repoProject.InsertAsync(entityProject);
            await repoUser.InsertAsync(entityUser);
            await workUnit.CommitAsync();
            //Assert
            Assert.True(await repoUser.ExistsAsync(entityUser));
            Assert.True(await repoProject.ExistsAsync(entityProject));
            Assert.True(await repoActivity.ExistsAsync(entityActivity));
        }
    }
}
