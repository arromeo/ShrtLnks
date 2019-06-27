using Xunit;
using Moq;
using ShrtLnks.Data;
using ShrtLnks.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShrtLnks.Models;
using System.Collections.Generic;
using System;

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

        private static List<Link> GetTestLinks()
        {
            var links = new List<Link>();
            links.Add(new Link()
            {
                OwnerId = "abcdefg",
                LongUrl = "http://example.com",
                ShortUrl = "b3fSsm9",
                CreateAt = new DateTime(2019, 1, 1)
            });
            links.Add(new Link()
            {
                OwnerId = "Anonymous",
                LongUrl = "http://google.com",
                ShortUrl = "asdfad9",
                CreateAt = new DateTime(2019, 1, 2)
            });

            return links;
        }
    }
}