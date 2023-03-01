using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Tests
{
    public class UserTests
    {
        private readonly TimeTrackerDbContext _dbContextSUT;

        public UserTests()
        {
            _dbContextSUT = new TimeTrackerDbContext();
        }
        [Fact]
        public void Add_New_User()
        {
            var user = new UserEntity()
            {
                FirstName = "John",
                LastName = "Doe",
                ImgUri = "https://www.google.com"
            };

            _dbContextSUT.Users.Add(user);
            _dbContextSUT.SaveChanges();
        }
    }
}