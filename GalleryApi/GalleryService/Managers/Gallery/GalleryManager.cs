using GalleryService.Managers.Albums;
using GalleryService.Managers.Photos;
using GalleryService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryService.Managers.Gallery
{
    public class GalleryManager : IGalleryManager
    {
        private readonly IAlbumManager _albumManager;
        private readonly IPhotoManager _photoManager;

        public GalleryManager(IAlbumManager albumManager, IPhotoManager photoManager)
        {
            _albumManager = albumManager;
            _photoManager = photoManager;
        }

        public async Task<GalleryResponse> GetGalleryAsync()
        {
            var albums = await _albumManager.GetAlbumsAsync();
            var photos = await _photoManager.GetPhotosAsync();

            var gallery = new GalleryResponse
            {
                Albums = albums,
                Photos = photos
            };

            return gallery;
        }

        public async Task<GalleryResponse> GetGalleryForUserAsync(int userId)
        {
            var gallery = new GalleryResponse();
            var albums = await _albumManager.GetAlbumsForUserAsync(userId);

            var galleryAlbums = albums.ToList();

            if (!galleryAlbums.Any())
                return gallery;

            var albumsId = galleryAlbums.Select(a => a.Id);
            gallery.Albums = galleryAlbums;

            var galleryPhotos = await _photoManager.GetPhotosAsync();
            var allPhotos = galleryPhotos.ToList();

            if (!allPhotos.Any())
                return gallery;

            var photos = allPhotos.Where(p => albumsId.Contains(p.AlbumId)).ToList();
            if (photos.Any())
            {
                gallery.Photos = photos;
            }

            return gallery;
        }
    }
}