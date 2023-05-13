using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.BL.Mappers;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.UnitOfWork;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests.FacadesTests
{
    public class ProjectFacadeTests : FacadeTestsBase
    {
        private readonly ProjectFacade _projectFacade;
        private readonly ActivityFacade _activityFacade;


        public ProjectFacadeTests(ITestOutputHelper output) : base(output)
        {
            _projectFacade = new ProjectFacade(ProjectModelMapper, UnitOfWorkFactory);
            _activityFacade = new ActivityFacade(ActivityModelMapper, UnitOfWorkFactory);
        }

        [Fact]
        public async Task CreateSaveProject()
        {
            var testId = Guid.NewGuid();
            var expectedProject = new ProjectDetailModel()
            {
                Id = testId,
                Name = "Fancy name",
                Description = "Fancy description",
                CreatedById = UserSeeds.UserGet.Id,
                Activities = new List<ActivityListModel>().ToObservableCollection(),
                Users = new List<UserListModel>().ToObservableCollection(),
            };

            await _projectFacade.SaveAsync(expectedProject);
            var actualProject = await _projectFacade.GetAsync(testId);

            Assert.Equal((expectedProject.Id, expectedProject.Description, expectedProject.Name), (actualProject.Id, actualProject.Description, actualProject.Name));
        }

        [Fact]
        public async Task GetAllProjects()
        {
            var projects = await _projectFacade.GetAsync();
            Assert.Equal(projects.Count(), ProjectSeeds.NumProjects);
        }

        [Fact]
        public async Task GetSingleProject()
        {
            var project = await _projectFacade.GetAsync(ProjectSeeds.ProjectEntity1.Id);
            var expectedProject = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectEntity1);

            Assert.Equal((expectedProject.Id, expectedProject.Name, ActivitySeeds.NumActivities), (project.Id, project.Name, project.Activities.Count()));
        }

        [Fact]
        public async Task DeleteProject()
        {
            await _projectFacade.DeleteAsync(ProjectSeeds.ProjectEntity1.Id);
            var projects = await _projectFacade.GetAsync();
            Assert.NotEqual(ProjectSeeds.NumProjects, projects.Count());
        }

        [Fact]
        public async Task DeleteProjectRelatedActivitiesCascade()
        {
            await _projectFacade.DeleteAsync(ProjectSeeds.ProjectEntity1.Id);
            var activities = await _activityFacade.GetAsync();
            Assert.NotEqual(ActivitySeeds.NumActivities, activities.Count());
        }
        [Fact]
        public async Task DeleteProjectRelatedActivitiesNotCascadingUnrelated()
        {
            await _projectFacade.DeleteAsync(ProjectSeeds.ProjectGet.Id);
            var activities = await _activityFacade.GetAsync();
            Assert.Equal(ActivitySeeds.NumActivities, activities.Count());
        }

        [Fact]
        public async Task UpdateProject()
        {
            var retrievedProject = await _projectFacade.GetAsync(ProjectSeeds.ProjectEntity1.Id);
            if (retrievedProject == null)
            {
                throw new Exception("Project not found");
            }
            retrievedProject.Name = "Updated name";
            await _projectFacade.SaveAsync(retrievedProject.WithoutRelatedProperties());
            var finalProject = await _projectFacade.GetAsync(ProjectSeeds.ProjectEntity1.Id);
            if (finalProject == null)
            {
                throw new Exception("Project not found");
            }

            Assert.Equal(retrievedProject.Name, finalProject.Name);
        }

        [Fact]
        public async Task AddUserToProject()
        {
            await _projectFacade.AddUserToProjectAsync(ProjectSeeds.ProjectUpdate.Id, UserSeeds.UserUpdate.Id);
            await _projectFacade.AddUserToProjectAsync(ProjectSeeds.ProjectUpdate.Id, UserSeeds.UserGet.Id);
            var project = await _projectFacade.GetAsync(ProjectSeeds.ProjectUpdate.Id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }

            if (project.Users == null)
            {
                throw new Exception("No users");
            }
            Assert.Equal(2, project.Users.Count());

        }

        [Fact]
        public async Task removeuserFromProject()
        {
            await _projectFacade.AddUserToProjectAsync(ProjectSeeds.ProjectUpdate.Id, UserSeeds.UserUpdate.Id);
            await _projectFacade.AddUserToProjectAsync(ProjectSeeds.ProjectUpdate.Id, UserSeeds.UserGet.Id);

            await _projectFacade.RemoveUserFromProjectAsync(ProjectSeeds.ProjectUpdate.Id, UserSeeds.UserUpdate.Id);

            var project = await _projectFacade.GetAsync(ProjectSeeds.ProjectUpdate.Id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }

            if (project.Users == null)
            {
                throw new Exception("No users");
            }
            Assert.Single(project.Users);
        }
    }
}
