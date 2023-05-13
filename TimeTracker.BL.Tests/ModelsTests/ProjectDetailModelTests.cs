using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ProjectDetailModelTests
    {
        [Fact]
        public void NameProperty_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var project = new ProjectDetailModel
            {
                // Act
                Name = "Test Project"
            };

            // Assert
            Assert.Equal("Test Project", project.Name);
        }

        [Fact]
        public void DescriptionProperty_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var project = new ProjectDetailModel
            {
                // Act
                Description = "Test Description"
            };

            // Assert
            Assert.Equal("Test Description", project.Description);
        }

        [Fact]
        public void CreatedByIdProperty_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var userId = new Guid("01234567-89ab-cdef-0123-456789abcdef");
            var project = new ProjectDetailModel
            {
                Name = String.Empty,
                Description = String.Empty,
                CreatedById = new Guid("01234567-89ab-cdef-0123-456789abcdef")
            };

            // Assert
            Assert.Equal(userId, project.CreatedById);
        }

        [Fact]
        public void ActivitiesProperty_ShouldBeInitializedProperly()
        {
            // Arrange
            var project = new ProjectDetailModel();

            // Assert
            Assert.NotNull(project.Activities);
            Assert.IsType<ObservableCollection<ActivityListModel>>(project.Activities);
        }

        [Fact]
        public void UsersProperty_ShouldBeInitializedProperly()
        {
            // Arrange
            var project = new ProjectDetailModel();

            // Assert
            Assert.NotNull(project.Users);
            Assert.IsType<ObservableCollection<UserListModel>>(project.Users);
        }

        [Fact]
        public void ActivitiesProperty_ShouldBeAbleToAddNewItems()
        {
            // Arrange
            var project = new ProjectDetailModel();
            var activity = new ActivityListModel
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now, 
                End = DateTime.Now,
                Type = ActivityType.Empty,

            };

            // Act
            project.Activities.Add(activity);

            // Assert
            Assert.Single(project.Activities);
            Assert.Contains(activity, project.Activities);
        }

        [Fact]
        public void UsersProperty_ShouldBeAbleToAddNewItems()
        {
            // Arrange
            var project = new ProjectDetailModel();
            var user = new UserListModel { Id = System.Guid.NewGuid(), FirstName = "Ignac", ImgUri = "jo", LastName = "Chlebodarce"};

            // Act
            project.Users.Add(user);

            // Assert
            Assert.Single(project.Users);
            Assert.Equal(user, project.Users[0]);
        }

        [Fact]
        public void EmptyProperty_ShouldReturnProjectDetailModelWithAllPropertiesSetToDefaults()
        {
            // Act
            var emptyProject = ProjectDetailModel.Empty;

            // Assert
            Assert.Equal(System.Guid.Empty, emptyProject.Id);
            Assert.Equal(string.Empty, emptyProject.Name);
            Assert.Equal(string.Empty, emptyProject.Description);
            Assert.Equal(System.Guid.Empty, emptyProject.CreatedById);
            Assert.NotNull(emptyProject.Activities);
            Assert.Empty(emptyProject.Activities);
            Assert.NotNull(emptyProject.Users);
            Assert.Empty(emptyProject.Users);
        }
    }
}
