using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.Common.Tests.Seeds;
using TimeTracker.DAL.Entities;
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
        [Fact]
        public async Task UserFacadeTestInsert()
        {
            var facade = new UserFacade(new Mappers.UserModelMapper(), UnitOfWorkFactory);
            UserEntity entity = new()
            {
                Id = Guid.Parse("10000000-0000-aaaa-0000-000000000003"),
                FirstName = "Igor",
                LastName = "Rogi",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            var dmodel = UserModelMapper.MapToDetailModel(entity);
            Assert.NotNull(dmodel);

            var real = await facade.SaveAsync(dmodel);
            var userEnum = await facade.GetAsync();
            Assert.NotNull(userEnum);

            var fmodel = userEnum.Where(i => i.Id == real.Id).ToList().First();
            var lmodel = UserModelMapper.MapToListModel(UserModelMapper.MapToEntity(real));
            Assert.Equal(fmodel, lmodel);
        }
        [Fact]
        public async Task UserFacadeTestUpdate()
        {
            var facade = new UserFacade(new Mappers.UserModelMapper(), UnitOfWorkFactory);
            UserEntity entity = new()
            {
                Id = UserSeeds.UserUpdate.Id,
                FirstName = "Igor2",
                LastName = "Rogi2",
                ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
            };
            var dmodel = UserModelMapper.MapToDetailModel(entity);
            Assert.NotNull(dmodel);

            var real = await facade.SaveAsync(dmodel);
            var userEnum = await facade.GetAsync();
            Assert.NotNull(userEnum);

            var fmodel = userEnum.Where(i => i.Id == real.Id).ToList().First();
            var lmodel = UserModelMapper.MapToListModel(UserModelMapper.MapToEntity(real));
            Assert.Equal(fmodel, lmodel);
        }
    }
}
