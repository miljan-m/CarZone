using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Models;

namespace CarZone.Application.Services
{
    public class ImageService : IImageService
    {
        private IImageRepository _repository;
        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _repository.GetAllImages();
        }

        public async Task<IEnumerable<Image>> GetImageByListingId(int listingId)
        {
            return await _repository.GetImageByListingId(listingId);
        }
    }
}