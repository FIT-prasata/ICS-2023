using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ActivityDetailModelTests
    {
        [Fact]
        public void Empty_Always_ReturnsEmptyActivityDetailModel()
        {
            // Act
            var emptyActivity = ActivityDetailModel.Empty;

            // Assert
            Assert.Equal(Guid.Empty, emptyActivity.Id);
            Assert.Equal(DateTime.Now.Date, emptyActivity.Start.Date);
            Assert.Equal(DateTime.Now.Date, emptyActivity.End.Date);
            Assert.Equal(string.Empty, emptyActivity.Description);
            Assert.Equal(ActivityType.Empty, emptyActivity.Type);
            Assert.Equal(UserDetailModel.Empty, emptyActivity.CreatedBy);
            Assert.Equal(emptyActivity.Assigned, UserDetailModel.Empty);
            Assert.Equal(Guid.Empty, emptyActivity.ProjectId);
        }

        [Fact]
        public void Start_End_AssignedId_ProjectId_CanBeSet()
        {
            // Arrange
            var activity = new ActivityDetailModel();

            // Act
            activity.Start = new DateTime(2023, 04, 08, 9, 0, 0);
            activity.End = new DateTime(2023, 04, 08, 10, 0, 0);
            activity.Assigned = new UserModelMapper().MapToDetailModel(UserSeeds.UserGet);
            activity.ProjectId = new Guid("01234567-89ab-cdef-0123-456789aeeeee");

            // Assert
            Assert.Equal(new DateTime(2023, 04, 08, 9, 0, 0), activity.Start);
            Assert.Equal(new DateTime(2023, 04, 08, 10, 0, 0), activity.End);
            Assert.NotNull(activity.Assigned);
            Assert.Equal(UserSeeds.UserGet, new UserModelMapper().MapToEntity(activity.Assigned));
            Assert.Equal(new Guid("01234567-89ab-cdef-0123-456789aeeeee"), activity.ProjectId);
        }

        [Fact]
        public void Type_CanBeSetToValidValue()
        {
            // Arrange
            var activity = new ActivityDetailModel();

            // Act
            activity.Type = ActivityType.Work;

            // Assert
            Assert.Equal(ActivityType.Work, activity.Type);
        }

        [Fact]
        public void CreatedById_CanBeSet()
        {
            // Arrange
            var activity = new ActivityDetailModel();

            // Act
            activity.CreatedBy = new UserModelMapper().MapToDetailModel(UserSeeds.UserGet);

            // Assert
            Assert.Equal(UserSeeds.UserGet, new UserModelMapper().MapToEntity(activity.CreatedBy));
        }

        [Fact]
        public void Description_CanBeSet()
        {
            // Arrange
            var activity = new ActivityDetailModel();

            // Act
            activity.Description = "Some description";

            // Assert
            Assert.Equal("Some description", activity.Description);
        }
    }
}
