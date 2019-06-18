using GalleryService.Controllers;
using GalleryService.Managers.Gallery;
using GalleryService.Models;
using GalleryService.Tests.Fakes;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GalleryService.Tests.UnitTests
{
    public class GalleryControllerTests
    {
        private readonly GalleryController _controller;

        public GalleryControllerTests()
        {
            IGalleryManager galleryManager = new GalleryManagerFake();
            _controller = new GalleryController(galleryManager);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllAlbumsAndPhotos()
        {
            // Act
            var result = await _controller.Get();
            var okResult = result.Result as OkObjectResult;

            // Assert
            var gallery = Assert.IsType<GalleryResponse>(okResult.Value);
            Assert.Equal(2, gallery.Albums.Count());
            Assert.Equal(2, gallery.Photos.Count());
        }

        [Fact]
        public async Task GetById_ExistingUserIdPassed_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;

            // Act
            var okResult = await _controller.Get(userId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task GetById_ExistingUserIdPassed_ReturnsAlbumsAndPhotosForUser()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = await _controller.Get(userId);
            var okResult = result.Result as OkObjectResult;

            // Assert
            var gallery = Assert.IsType<GalleryResponse>(okResult.Value);
            Assert.Single(gallery.Albums);
            Assert.Equal(2, gallery.Photos.Count());
            Assert.Equal("quidem molestiae enim", gallery.Albums.FirstOrDefault()?.Title);
        }

        [Fact]
        public async Task GetById_UnknownUserIdPassed_ReturnsNotFoundResult()
        {
            //Arrange
            var userId = 999;

            // Act
            var notFoundResult = await _controller.Get(userId);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }
    }
}