using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.Entities;
using Xunit.Abstractions;

namespace TimeTracker.DAL.Tests
{
    public class ProjectTests : DbContextTestsBase
    {
        public ProjectTests(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public async Task AddNew_Project()
        {
            //Arrange            
            ProjectEntity entity = new()
            {
                Id = Guid.NewGuid(),
                CreatedById = UserSeeds.UserEntity1.Id,
                Name = "New Project 1",
            };

            //Act
            TimeTrackerDbContextSUT.Projects.Add(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity,actualEntities);
        }

        [Fact]
        public async Task GetAllProjects_ContainsSeededProject()
        {
            //Act
            var entities = await TimeTrackerDbContextSUT.Projects.ToArrayAsync();
            //Assert
            Assert.Contains(ProjectSeeds.ProjectGet, entities);
        }

        [Fact]
        public async Task GetProjectById()
        {
            //Act
            var entity = await TimeTrackerDbContextSUT.Projects.FindAsync(ProjectSeeds.ProjectGet.Id);
            //Assert
            Assert.Equal(ProjectSeeds.ProjectGet, entity);
        }

        [Fact]
        public async Task UpdateProject()
        {
            //Arrange
            var baseEntity = ProjectSeeds.ProjectUpdate;
            var entity =
                baseEntity with
                {
                    Name = baseEntity + "Updated",
                    Description = baseEntity + "Updated",
                };

            //Act
            TimeTrackerDbContextSUT.Projects.Update(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task DeleteProject()
        {
            //Arrange
            var entityBase = ProjectSeeds.ProjectDelete;

            //Act
            TimeTrackerDbContextSUT.Projects.Remove(entityBase);
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TimeTrackerDbContextSUT.Projects.AnyAsync(i => i.Id == entityBase.Id));
        }

        [Fact]
        public async Task DeleteProjectById()
        {
            //Arrange
            var entityBase = ProjectSeeds.ProjectDelete;

            //Act
            TimeTrackerDbContextSUT.Remove(
                TimeTrackerDbContextSUT.Projects.Single(i => i.Id == entityBase.Id));
            await TimeTrackerDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TimeTrackerDbContextSUT.Projects.AnyAsync(i => i.Id == entityBase.Id));
        }
    }
}
