using System.Collections.Generic;

namespace GalleryService.Models
{
    public class GalleryResponse
    {
        public IEnumerable<Album> Albums { get; set; }

        public IEnumerable<Photo> Photos { get; set; }
    }
}