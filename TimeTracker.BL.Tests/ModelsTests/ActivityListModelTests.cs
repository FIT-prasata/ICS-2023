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
            Assert.Equal(UserDetailModel.Empty, emptyActivity.Assigned);
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
                Assigned = new UserModelMapper().MapToDetailModel(UserSeeds.UserGet),
            };

            // Act & Assert
            Assert.Equal(activity.Id, activity.Id);
            Assert.Equal(activity.Start, activity.Start);
            Assert.Equal(activity.End, activity.End);
            Assert.Equal(activity.Type, activity.Type);
            Assert.Equal(activity.Assigned.Id, activity.Assigned.Id);
        }
    }
}
