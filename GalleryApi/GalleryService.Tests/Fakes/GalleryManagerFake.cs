using GalleryService.Managers.Gallery;
using GalleryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryService.Tests.Fakes
{
    public class GalleryManagerFake : IGalleryManager
    {
        private readonly GalleryResponse _galleryResponse;

        public GalleryManagerFake()
        {
            var albums = new List<Album>
            {
                new Album
                {
                    Id = 1,
                    UserId = 1,
                    Title = "quidem molestiae enim"
                },
                new Album
                {
                    Id = 2,
                    UserId = 2,
                    Title = "sunt qui excepturi placeat culpa"
                }
            };

            var photos = new List<Photo>
            {
                new Photo
                {
                    Id =1,
                    AlbumId =  1,
                    Title = "accusamus beatae ad facilis cum similique qui sunt",
                    Url = "https://via.placeholder.com/600/92c952",
                    ThumbnailUrl = "https://via.placeholder.com/150/92c952"
                },
                new Photo
                {
                    Id =2,
                    AlbumId =  1,
                    Title = "reprehenderit est deserunt velit ipsam",
                    Url = "https://via.placeholder.com/600/771796",
                    ThumbnailUrl = "https://via.placeholder.com/150/771796"
                }
            };

            _galleryResponse = new GalleryResponse
            {
                Albums = albums,
                Photos = photos
            };
        }

        public Task<GalleryResponse> GetGalleryAsync()
        {
            return Task.FromResult(_galleryResponse);
        }

        public Task<GalleryResponse> GetGalleryForUserAsync(int userId)
        {
            GalleryResponse gallery = null;

            var albums = _galleryResponse.Albums.Where(a => a.UserId == userId);
            var galleryAlbums = albums.ToList();
            if (galleryAlbums.Any())
            {
                gallery = new GalleryResponse { Albums = galleryAlbums};
                var albumIds = gallery.Albums.Select(a => a.Id);
                gallery.Photos = _galleryResponse.Photos.Where(a => albumIds.Contains(a.AlbumId));
            }

            return Task.FromResult(gallery);
        }
    }
}