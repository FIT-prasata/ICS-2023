using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ProjectUserDetailModelTests
    {
        [Fact]
        public void Empty_Returns_Model_With_Default_Values()
        {
            // Arrange
            var expected = new ProjectUserDetailModel
            {
                Id = Guid.Empty,
            };

            // Act
            var actual = ProjectUserDetailModel.Empty;

            // Assert
            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public void Properties_Set_Correctly()
        {
            // Arrange
            var model = new ProjectUserDetailModel();
            var expectedId = Guid.NewGuid();

            // Act
            model.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, model.Id);
        }
    }
}
