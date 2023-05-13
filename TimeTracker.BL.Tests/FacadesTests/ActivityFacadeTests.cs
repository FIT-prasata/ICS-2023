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
        public async Task Create_Save_Activity()
        {
            Guid testId = Guid.NewGuid();
            var activity = new ActivityDetailModel()
            {
                Id = testId,
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Fancy description",
                Type = ActivityType.Work,
                CreatedBy = new UserModelMapper().MapToDetailModel(UserSeeds.UserGet),
                Assigned =  new UserModelMapper().MapToDetailModel(UserSeeds.UserDelete),
                ProjectId = ProjectSeeds.ProjectGet.Id,
            };
            var saved = await _activityFacade.SaveAsync(activity);

            var expected_activity = await _activityFacade.GetAsync(testId);
            var lmodel = ActivityModelMapper.MapToDetailModel(ActivityModelMapper.MapToEntity(saved));
            Assert.Equal(expected_activity, lmodel);
        }

        [Fact]
        public async Task Get_All_Activities()
        {
            IEnumerable<ActivityListModel> activities = await _activityFacade.GetAsync();
            Assert.Equal(activities.Count(), ActivitySeeds.NumActivities);
        }

        [Fact]
        public async Task Get_Predefined_Activity()
        {
            var activity = await _activityFacade.GetAsync(ActivitySeeds.ActivityGet.Id);
            ActivityDetailModel emodel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityGet);
            Assert.Equal(emodel, activity);
        }

        [Fact]
        public async Task Get_Activity_By_Id()
        {
            var activity = await _activityFacade.GetAsync(ActivitySeeds.ActivityGet.Id);
            ActivityDetailModel emodel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityGet);
            Assert.Equal(emodel, activity);
        }

        [Fact]
        public async Task Delete_Activity_By_Id()
        {
            await _activityFacade.DeleteAsync(ActivitySeeds.ActivityDelete.Id);
            var activities = await _activityFacade.GetAsync();
            Assert.NotEqual(activities.Count(), ActivitySeeds.NumActivities);

        }


    }
}
