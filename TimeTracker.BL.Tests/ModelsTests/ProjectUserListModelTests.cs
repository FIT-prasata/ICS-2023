using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ProjectUserListModelTests
    {
        [Fact]
        public void Empty_Returns_Model_With_Default_Values()
        {
            // Arrange
            var expected = new ProjectUserListModel
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                ProjectId = Guid.Empty,
            };

            // Act
            var actual = ProjectUserListModel.Empty;

            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.Equal(expected.ProjectId, actual.ProjectId);
        }

        [Fact]
        public void Properties_Set_Correctly()
        {
            // Arrange
            var model = new ProjectUserListModel();
            var expectedUserId = Guid.NewGuid();
            var expectedProjectId = Guid.NewGuid();

            // Act
            model.UserId = expectedUserId;
            model.ProjectId = expectedProjectId;

            // Assert
            Assert.Equal(expectedUserId, model.UserId);
            Assert.Equal(expectedProjectId, model.ProjectId);
        }
    }
}
