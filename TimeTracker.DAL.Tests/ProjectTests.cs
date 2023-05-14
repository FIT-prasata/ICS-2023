using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.Repositories;
using TimeTracker.DAL.Seeds;
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
        [Fact]
        public async Task ProjectRepositoryExists()
        {
            //Arrange
            var repo = new Repository<ProjectEntity>(TimeTrackerDbContextSUT, new ProjectEntityMapper());
            var entity = ProjectSeeds.ProjectGet;

            //Assert
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task ProjectRepositoryInsert()
        {
            //Arrange
            ProjectEntity entity = new()
            {
                Id = Guid.NewGuid(),
                CreatedById = UserSeeds.UserEntity1.Id,
                Name = "New Project 1",
            };
            var repo = new Repository<ProjectEntity>(TimeTrackerDbContextSUT, new ProjectEntityMapper());

            Assert.False(await repo.ExistsAsync(entity));

            //Act
            await repo.InsertAsync(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.True(await repo.ExistsAsync(entity));
        }
        [Fact]
        public async Task ProjectRepositoryUpdate()
        {
            //Arrange
            ProjectEntity entity = ProjectSeeds.ProjectUpdate;
            entity.Name = entity.Name + "Updated";

            var repo = new Repository<ProjectEntity>(TimeTrackerDbContextSUT, new ProjectEntityMapper());

            Assert.True(await repo.ExistsAsync(entity));

            //Act
            await repo.UpdateAsync(entity);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            var entityU = repo.Get().Where(i => i.Id == entity.Id).ToList().First();
            //Assert
            Assert.Equal(entity, entityU);
        }
        [Fact]
        public async Task ProjectRepositoryDelete()
        {
            //Arrange
            ProjectEntity entity = ProjectSeeds.ProjectDelete;

            var repo = new Repository<ProjectEntity>(TimeTrackerDbContextSUT, new ProjectEntityMapper());

            Assert.True(await repo.ExistsAsync(entity));

            //Act
            repo.Delete(entity.Id);
            await TimeTrackerDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.False(await repo.ExistsAsync(entity));
        }
    }
}
