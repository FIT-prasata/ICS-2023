using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Enums;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.Repositories;
using TimeTracker.DAL.UnitOfWork;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests.FacadesTests
{
    public class ActivityFacadeTests : FacadeTestsBase
    {
        private readonly ActivityFacade _activityFacade;

        public ActivityFacadeTests(ITestOutputHelper output) : base(output)
        {
            _activityFacade = new ActivityFacade(ActivityModelMapper, UnitOfWorkFactory);
        }

        [Fact]
        public async Task CreateSaveActivity()
        {

            var testId = Guid.NewGuid();
            var expectedActivity = new ActivityDetailModel()
            {
                Id = testId,
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Fancy description",
                Type = ActivityType.Work,
                CreatedBy = new UserModelMapper().MapToDetailModel(UserSeeds.UserGet),
                Assigned = new UserModelMapper().MapToDetailModel(UserSeeds.UserDelete),
                ProjectId = ProjectSeeds.ProjectGet.Id,
            };


            await _activityFacade.SaveAsync(expectedActivity);
            var actualActivity = await _activityFacade.GetAsync(testId);


            Assert.Equal(expectedActivity, actualActivity);
        }

        [Fact]
        public async Task GetAllActivities()
        {
            var activities = await _activityFacade.GetAsync();
            Assert.Equal(activities.Count(), ActivitySeeds.NumActivities);
        }

        [Fact]
        public async Task GetSingleActivity()
        {
            var activity = await _activityFacade.GetAsync(ActivitySeeds.ActivityGet.Id);
            var expectedActivity = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityGet);

            Assert.Equal((expectedActivity.Id, expectedActivity.Start, expectedActivity.End, expectedActivity.Description), (activity.Id, activity.Start, activity.End, activity.Description));
        }


        [Fact]
        public async Task DeleteActivityBy()
        {
            await _activityFacade.DeleteAsync(ActivitySeeds.ActivityDelete.Id);
            var activities = await _activityFacade.GetAsync();
            Assert.NotEqual(activities.Count(), ActivitySeeds.NumActivities);

        }

        [Fact]
        public async Task UpdateActivity()
        {
            var retrievedActivity = await _activityFacade.GetAsync(ActivitySeeds.ActivityUpdate.Id);
            if (retrievedActivity == null)
            {
                throw new Exception("Activity not found");
            }
            retrievedActivity.Description = "Updated description";
            retrievedActivity.Assigned = new UserModelMapper().MapToDetailModel(UserSeeds.UserDelete);
            await _activityFacade.SaveAsync(retrievedActivity);
            var finalActivity = await _activityFacade.GetAsync(ActivitySeeds.ActivityUpdate.Id);

            Assert.Equal(retrievedActivity, finalActivity);

        }


    }
}
