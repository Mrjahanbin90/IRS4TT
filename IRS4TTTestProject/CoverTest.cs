using IRS4TT.Controllers;
using IRS4TT.Domains;
using IRS4TT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace IRS4TT.Tests
{
    public class CoverControllerTests
    {
        private readonly Mock<ICoverService> _mockService;
        private readonly CoverController _controller;

        public CoverControllerTests()
        {
            _mockService = new Mock<ICoverService>();
            _controller = new CoverController(_mockService.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfCovers()
        {
            _mockService.Setup(s => s.GetAll()).Returns(new List<Cover> {
                new Cover { Id = 1, TitleEn = "Fire", TitleFa = "آتش" }
            });

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Cover>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Details_IdNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(5)).Returns((Cover)null);

            var result = _controller.Details(5);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ValidCover_RedirectsToIndex()
        {
            var cover = new Cover { Id = 1, TitleEn = "Fire", TitleFa = "آتش" };

            var result = _controller.Create(cover);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _mockService.Verify(s => s.Add(cover), Times.Once);
        }

        [Fact]
        public void Edit_IdMismatch_ReturnsBadRequest()
        {
            var result = _controller.Edit(2, new Cover { Id = 1 });

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteConfirmed_DeletesAndRedirects()
        {
            var result = _controller.DeleteConfirmed(3);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _mockService.Verify(s => s.Delete(3), Times.Once);
        }
    }
}

