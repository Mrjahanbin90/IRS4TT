using IRS4TT.Controllers;
using IRS4TT.Domains;
using IRS4TT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IRS4TT.Tests
{
    public class CoverGroupTests
    {
        private readonly Mock<ICoverGroupService> _mockService;
        private readonly CoverGroupController _controller;

        public CoverGroupTests()
        {
            _mockService = new Mock<ICoverGroupService>();
            _controller = new CoverGroupController(_mockService.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithCoverGroupList()
        {
            _mockService.Setup(s => s.GetAll()).Returns(new List<CoverGroup> {
                new CoverGroup { Id = 1, Title = "Engineering" }
            });

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CoverGroup>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Details_NotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(99)).Returns((CoverGroup)null);

            var result = _controller.Details(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ValidModel_RedirectsToIndex()
        {
            var model = new CoverGroup { Id = 1, Title = "Medical" };

            var result = _controller.Create(model);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _mockService.Verify(s => s.Add(model), Times.Once);
        }

        [Fact]
        public void Edit_IdMismatch_ReturnsBadRequest()
        {
            var result = _controller.Edit(2, new CoverGroup { Id = 1 });

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
