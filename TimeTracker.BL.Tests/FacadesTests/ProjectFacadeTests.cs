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

        public ProjectFacadeTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task ProjectFacadeTestGet()
        {
            var facade = new ProjectFacade(new ProjectModelMapper(new ActivityModelMapper(new UserModelMapper()), new UserModelMapper()), UnitOfWorkFactory);
            var projectEnum = await facade.GetAsync();
            Assert.NotNull(projectEnum);
            var fmodel = projectEnum.Where(i => i.Id == ProjectSeeds.ProjectGet.Id).ToList().First();
            ProjectListModel emodel = ProjectModelMapper.MapToListModel(ProjectSeeds.ProjectGet);
            Assert.NotNull(emodel);
            Assert.Equal(fmodel, emodel);
        }

        //[Fact]
        //public async Task ProjectFacadeTestGetById()
        //{
        //    var facade = new ProjectFacade(new ProjectModelMapper(new ActivityModelMapper(), new ProjectUserModelMapper()), UnitOfWorkFactory);
        //    var projectEnum = await facade.GetAsync(ProjectSeeds.ProjectGet.Id);
        //    Assert.NotNull(projectEnum);
        //    ProjectDetailModel fmodel = projectEnum;
        //    ProjectDetailModel emodel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.ProjectGet);
        //    Assert.NotNull(emodel);
        //    Assert.Equal(fmodel, emodel);
        //}


    }
}
