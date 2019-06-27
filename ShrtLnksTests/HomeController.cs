using Xunit;
using Moq;
using ShrtLnks.Data;
using ShrtLnks.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShrtLnksTests
{
    public class TheHomeController
    {
        [Fact]
        public void IndexReturnsView()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new HomeController(_dbContext);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}