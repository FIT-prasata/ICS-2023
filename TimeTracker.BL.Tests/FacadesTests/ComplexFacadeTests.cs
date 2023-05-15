﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Enums;
using TimeTracker.DAL.Seeds;
using Xunit.Abstractions;

namespace TimeTracker.BL.Tests.FacadesTests
{
    public class ComplexFacadeTests: FacadeTestsBase
    {
        private readonly ProjectFacade _projectFacade;
        private readonly UserFacade _userFacade;
        private readonly ActivityFacade _activityFacade;

        public ComplexFacadeTests(ITestOutputHelper output): base(output)
        {
            _projectFacade = new ProjectFacade(ProjectModelMapper, UnitOfWorkFactory);
            _userFacade = new UserFacade(UserModelMapper, UnitOfWorkFactory);
            _activityFacade = new ActivityFacade(ActivityModelMapper, UnitOfWorkFactory);
        }

        [Fact]
        public async Task CreateActivityAndAccessItThroughProject()
        {
            var activityId = Guid.NewGuid();
            var expectedActivity = new ActivityDetailModel()
            {
                Id = activityId,
                Start = DateTime.Now,
                End = DateTime.Now,
                Description = "Fancy description",
                Type = ActivityType.Work,
                CreatedBy = new UserModelMapper().MapToDetailModel(UserSeeds.UserGet),
                Assigned = new UserModelMapper().MapToDetailModel(UserSeeds.UserDelete),
                ProjectId = ProjectSeeds.ProjectGet.Id,
            };
            await _activityFacade.SaveAsync(expectedActivity);

            var project = await _projectFacade.GetAsync(ProjectSeeds.ProjectGet.Id);
            if (project == null)
            {
                throw new Exception("Project not found");
            }
            var actualActivity = project.Activities == null ? throw new Exception("Activities not found") : project.Activities.FirstOrDefault(a => a.Id == activityId);
            var expectedActivityListModel = ActivityModelMapper.MapToListModel(ActivityModelMapper.MapToEntity(expectedActivity));
            Assert.Equal(expectedActivityListModel, actualActivity);
        }

        [Fact]
        public async Task GetUsersAssignedToProject()
        {
            await _projectFacade.AddUserToProjectAsync( ProjectSeeds.ProjectEntity1.Id, UserSeeds.UserEntity1.Id);
            await _projectFacade.AddUserToProjectAsync(ProjectSeeds.ProjectEntity1.Id, UserSeeds.UserGet.Id);
            await _projectFacade.AddUserToProjectAsync(ProjectSeeds.ProjectEntity1.Id, UserSeeds.UserUpdate.Id);

            var users = await _userFacade.GetByProjectAsync(ProjectSeeds.ProjectEntity1.Id);
            Assert.Equal(3, users.Count());

        }

    }
}
