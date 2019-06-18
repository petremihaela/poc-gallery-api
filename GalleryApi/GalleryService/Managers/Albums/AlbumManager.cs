using GalleryService.Constants;
using GalleryService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GalleryService.Managers.Albums
{
    public class AlbumManager : IAlbumManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public AlbumManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            var client = _clientFactory.CreateClient(GalleryConstants.GallerySourceProviderName);
            var albumsResponse = await client.GetAsync(GalleryConstants.AlbumsUri);
            var albumsResponseData = await albumsResponse.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(albumsResponseData);
            return albums;
        }

        public async Task<IEnumerable<Album>> GetAlbumsForUserAsync(int userId)
        {
            var client = _clientFactory.CreateClient(GalleryConstants.GallerySourceProviderName);
            var requestUri = $"{GalleryConstants.AlbumsUri}/?userId{userId}";
            var albumsResponse = await client.GetAsync(requestUri);
            var albumsResponseData = await albumsResponse.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(albumsResponseData);
            return albums;
        }
    }
}