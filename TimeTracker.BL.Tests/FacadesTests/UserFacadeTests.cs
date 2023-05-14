using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Seeds;
using TimeTracker.DAL.UnitOfWork;
using Xunit.Abstractions;


namespace TimeTracker.BL.Tests.FacadesTests
{
    public class UserFacadeTests : FacadeTestsBase
    {
        protected readonly UserFacade _userFacade;
        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
            _userFacade = new UserFacade(UserModelMapper, UnitOfWorkFactory);
        }

        [Fact]
        public async Task CreateSaveUser()
        {
            var testId = Guid.NewGuid();
            var expectedUser = new UserDetailModel()
            {
                Id = testId,
                FirstName = "Jindrich",
                LastName = "Chlebonosic",
                ImgUri = "Jakoze-fotka.cz"
            };

            await _userFacade.SaveAsync(expectedUser);
            var actualUser = await _userFacade.GetAsync(testId);

            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public async Task GetAllUsers()
        {
            var users = await _userFacade.GetAsync();
            Assert.Equal(users.Count(), UserSeeds.NumUsers);
        }

        [Fact]
        public async Task GetSingleUser()
        {
            var user = await _userFacade.GetAsync(UserSeeds.UserGet.Id);
            var expectedUser = UserModelMapper.MapToDetailModel(UserSeeds.UserGet);
            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public async Task DeleteUser()
        {
            var user = await _userFacade.GetAsync(UserSeeds.UserDelete.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            await _userFacade.DeleteAsync(user.Id);
            var users = await _userFacade.GetAsync();
            Assert.NotEqual(users.Count(), UserSeeds.NumUsers);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var user = await _userFacade.GetAsync(UserSeeds.UserUpdate.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.FirstName = "Jindrich";
            user.LastName = "Chlebonosic";
            user.ImgUri = "Jakoze-fotka.cz";
            await _userFacade.SaveAsync(user);
            var updatedUser = await _userFacade.GetAsync(user.Id);
            Assert.Equal(user, updatedUser);
        }
        
    }
}
