using GalleryService.Constants;
using GalleryService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GalleryService.Managers.Photos
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public PhotoManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync()
        {
            var client = _clientFactory.CreateClient(GalleryConstants.GallerySourceProviderName);
            var response = await client.GetAsync(GalleryConstants.PhotosUri);
            var photosResponseData = await response.Content.ReadAsStringAsync();
            var photos = JsonConvert.DeserializeObject<IEnumerable<Photo>>(photosResponseData);
            return photos;
        }
    }
}