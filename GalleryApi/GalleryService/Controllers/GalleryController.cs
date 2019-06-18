using GalleryService.Managers.Gallery;
using GalleryService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GalleryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryManager _galleryManager;

        public GalleryController(IGalleryManager galleryManager)
        {
            _galleryManager = galleryManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<GalleryResponse>> Get()
        {
            var gallery = await _galleryManager.GetGalleryAsync();
            return Ok(gallery);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<GalleryResponse>> Get(int userId)
        {
            var gallery = await _galleryManager.GetGalleryForUserAsync(userId);
            return Ok(gallery);
        }
    }
}