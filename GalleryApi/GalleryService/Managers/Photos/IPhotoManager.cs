using GalleryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryService.Managers.Photos
{
    public interface IPhotoManager
    {
        Task<IEnumerable<Photo>> GetPhotosAsync();
    }
}