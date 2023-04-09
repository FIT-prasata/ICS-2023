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
            var activity = new ActivityDetailModel()
            {
                Id = Guid.Parse("10000000-0000-bbbb-0000-000000000003"),
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Fancy description",
                Type = ActivityType.Work,
                CreatedById = UserSeeds.UserGet.Id,
                AssignedId = UserSeeds.UserDelete.Id,
                ProjectId = ProjectSeeds.ProjectGet.Id,
            };
            var saved = await _activityFacade.SaveAsync(activity);

            var expected_activity = await _activityFacade.GetAsync();
            var expected_activity2 = expected_activity.Where((i => i.Id == saved.Id)).ToList().First();
            var lmodel = ActivityModelMapper.MapToListModel(ActivityModelMapper.MapToEntity(saved));
            Assert.Equal(expected_activity2, lmodel);
        }

        [Fact]
        public async Task Get_Predefined_Activity()
        {
            var activityEnum = await _activityFacade.GetAsync();
            var activity = activityEnum.Single(i => i.Id == ActivitySeeds.ActivityGet.Id);
            ActivityListModel emodel = ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityGet);
            Assert.Equal(emodel, activity);
        }

        [Fact]
        public async Task Get_Activity_By_Id()
        {
            var activity = await _activityFacade.GetAsync(ActivitySeeds.ActivityGet.Id);
            ActivityDetailModel emodel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityGet);
            Assert.Equal(emodel, activity);
        }
    }
}
