using GalleryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalleryService.Managers.Albums
{
    public interface IAlbumManager
    {
        Task<IEnumerable<Album>> GetAlbumsAsync();

        Task<IEnumerable<Album>> GetAlbumsForUserAsync(int userId);
    }
}