using GalleryService.Models;
using System.Threading.Tasks;

namespace GalleryService.Managers.Gallery
{
    public interface IGalleryManager
    {
        Task<GalleryResponse> GetGalleryAsync();

        Task<GalleryResponse> GetGalleryForUserAsync(int userId);
    }
}