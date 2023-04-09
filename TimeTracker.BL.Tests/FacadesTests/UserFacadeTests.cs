using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.UnitOfWork;
using Xunit.Abstractions;


namespace TimeTracker.BL.Tests.FacadesTests
{
    public class UserFacadeTests : FacadeTestsBase
    {
        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public async Task UserFacadeTestGet()
        {
            var facade = new UserFacade(new Mappers.UserModelMapper(), UnitOfWorkFactory);
            var userEnum = await facade.GetAsync();
            Assert.NotNull(userEnum);
            var fmodel = userEnum.Where(i => i.Id == UserSeeds.UserGet.Id).ToList().First();
            UserListModel emodel = UserModelMapper.MapToListModel(UserSeeds.UserGet);
            Assert.NotNull(emodel);
            Assert.Equal(fmodel,emodel);
        }
    }
}
