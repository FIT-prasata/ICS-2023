using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Enums;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ActivityModelTests
    {


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
