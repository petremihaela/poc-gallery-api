using Microsoft.AspNetCore.Mvc;

namespace GalleryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            //TODO implement
            return Ok();
        }
    }
}