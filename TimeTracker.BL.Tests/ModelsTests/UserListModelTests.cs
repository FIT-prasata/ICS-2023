using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class UserListModelTests
    {
        [Fact]
        public void Empty_Returns_UserListModel_With_Default_Values()
        {
            // Arrange
            var expected = new UserListModel
            {
                Id = Guid.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
            };

            // Act
            var actual = UserListModel.Empty;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserListModels_FullName_Returns_Correct_Format()
        {
            // Arrange
            var model = new UserListModel
            {
                FirstName = "John",
                LastName = "Doe",
            };

            // Act
            var actual = model.FullName;

            // Assert
            Assert.Equal("John Doe", actual);
        }

    }
}
