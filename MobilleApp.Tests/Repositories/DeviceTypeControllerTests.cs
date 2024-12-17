using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data;
using SellingMobileApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MobilleApp.Tests.Repositories
{
    public class DeviceTypeControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly DeviceTypeController _controller;

        public DeviceTypeControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _controller = new DeviceTypeController(_mockContext.Object);
        }

        [Fact]
        public void Phones_ReturnsViewWithPhones()
        {
            var userId = "user123";
            var deviceType = new DeviceType { Type = "Телефон" };
            var listings = new List<CreateListing>
            {
                new CreateListing
                {
                    Id = 1,
                    Title = "Phone1",
                    ImageUrl = "phone1.jpg",
                    Price = 100,
                    ReleaseDate = new DateTime(2024, 12, 12),
                    UserId = "user123",
                    User = new User { UserName = "user123" },
                    DeviceType = deviceType
                },
                new CreateListing
                {
                    Id = 2,
                    Title = "Phone2",
                    ImageUrl = "phone2.jpg",
                    Price = 200,
                    ReleaseDate = new DateTime(2024, 12, 12),
                    UserId = "user456",
                    User = new User { UserName = "user456" },
                    DeviceType = deviceType
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<CreateListing>>();
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.Provider).Returns(listings.Provider);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.Expression).Returns(listings.Expression);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.ElementType).Returns(listings.ElementType);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.GetEnumerator()).Returns(listings.GetEnumerator());

            _mockContext.Setup(c => c.CreateListings).Returns(mockDbSet.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) })) }
            };

            var result = _controller.Phones();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TypePhonesViewModel>>(viewResult.Model);
            Assert.Equal(2, model.Count);  
        }

        [Fact]
        public void Tablets_ReturnsViewWithTablets()
        {
            var userId = "user123";
            var deviceType = new DeviceType { Type = "Таблет" };
            var listings = new List<CreateListing>
            {
                new CreateListing
                {
                    Id = 1,
                    Title = "Tablet1",
                    ImageUrl = "tablet1.jpg",
                    Price = 150,
                    ReleaseDate = new DateTime(2024, 12, 12),
                    UserId = "user123",
                    User = new User { UserName = "user123" },
                    DeviceType = deviceType
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<CreateListing>>();
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.Provider).Returns(listings.Provider);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.Expression).Returns(listings.Expression);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.ElementType).Returns(listings.ElementType);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.GetEnumerator()).Returns(listings.GetEnumerator());

            _mockContext.Setup(c => c.CreateListings).Returns(mockDbSet.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) })) }
            };

            var result = _controller.Tablets();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TypeTabletsViewModel>>(viewResult.Model);
            Assert.Single(model);  
        }

        [Fact]
        public void Accessories_ReturnsViewWithAccessories()
        {
            var userId = "user123";
            var deviceType = new DeviceType { Type = "Аксесоари" };
            var listings = new List<CreateListing>
            {
                new CreateListing
                {
                    Id = 1,
                    Title = "Accessory1",
                    ImageUrl = "accessory1.jpg",
                    Price = 50,
                    ReleaseDate = new DateTime(2024, 12, 12),
                    UserId = "user123",
                    User = new User { UserName = "user123" },
                    DeviceType = deviceType
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<CreateListing>>();
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.Provider).Returns(listings.Provider);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.Expression).Returns(listings.Expression);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.ElementType).Returns(listings.ElementType);
            mockDbSet.As<IQueryable<CreateListing>>().Setup(m => m.GetEnumerator()).Returns(listings.GetEnumerator());

            _mockContext.Setup(c => c.CreateListings).Returns(mockDbSet.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) })) }
            };

            var result = _controller.Accessories();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TypeAccessoriesViewModel>>(viewResult.Model);
            Assert.Single(model);  
        }
    }
}
