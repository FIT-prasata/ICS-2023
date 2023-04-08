using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ActivityModelTests
    {
        [Fact]
        public void Empty_ReturnsValidInstance()
        {
            // Arrange & Act
            var emptyActivity = ActivityListModel.Empty;

            // Assert
            Assert.Equal(Guid.Empty, emptyActivity.Id);
            Assert.Equal(DateTime.Now.Date, emptyActivity.Start.Date);
            Assert.Equal(DateTime.Now.Date, emptyActivity.End.Date);
            Assert.Equal(ActivityType.Empty, emptyActivity.Type);
            Assert.Equal(Guid.Empty, emptyActivity.AssignedId);
            Assert.Equal(Guid.Empty, emptyActivity.ProjectId);
        }

        [Fact]
        public void Properties_SetAndGetCorrectly()
        {
            // Arrange
            var activity = new ActivityListModel()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now,
                Type = ActivityType.Empty,
                AssignedId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid()
            };

            // Act & Assert
            Assert.Equal(activity.Id, activity.Id);
            Assert.Equal(activity.Start, activity.Start);
            Assert.Equal(activity.End, activity.End);
            Assert.Equal(activity.Type, activity.Type);
            Assert.Equal(activity.AssignedId, activity.AssignedId);
            Assert.Equal(activity.ProjectId, activity.ProjectId);
        }
    }
}
