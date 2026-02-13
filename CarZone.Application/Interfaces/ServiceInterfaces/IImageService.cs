using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.ServiceInterfaces
{
    public interface IImageService
    {

        public Task<IEnumerable<Image>> GetAllImages();
        public Task<IEnumerable<Image>> GetImageByListingId(int listingId);

    }
}