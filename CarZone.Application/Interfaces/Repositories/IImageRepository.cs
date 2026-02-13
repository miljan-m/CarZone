using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.Repositories
{
    public interface IImageRepository
    {
        public Task<IEnumerable<Image>> GetAllImages();
        public Task<IEnumerable<Image>> GetImageByListingId(int listingId);
    }
}