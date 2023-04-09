using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Tests.ModelsTests
{
    public class ProjectListModelTests
    {
        [Fact]
        public void Empty_Returns_Model_With_Default_Values()
        {
            // Arrange
            var expected = new ProjectListModel
            {
                Id = Guid.Empty,
                Name = string.Empty,
                Description = string.Empty,
            };

            // Act
            var actual = ProjectListModel.Empty;

            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
        }

        [Fact]
        public void Properties_Set_Correctly()
        {
            // Arrange
            var model = new ProjectListModel();
            var expectedId = Guid.NewGuid();
            var expectedName = "Project 1";
            var expectedDescription = "A project description";

            // Act
            model.Id = expectedId;
            model.Name = expectedName;
            model.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedId, model.Id);
            Assert.Equal(expectedName, model.Name);
            Assert.Equal(expectedDescription, model.Description);
        }

        [Fact]
        public void Description_Property_Can_Be_Set_To_Null()
        {
            // Arrange
            var model = new ProjectListModel();

            // Act
            model.Description = null;

            // Assert
            Assert.Null(model.Description);
        }
    }
}
