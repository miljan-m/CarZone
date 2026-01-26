using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Domain.Enums;

namespace CarZone.Application.Interfaces.ServiceInterfaces
{
    public interface IListingService
    {
        
        public Task<GetListingDTO> GetListingById(int listingId);
        public Task<IEnumerable<GetListingDTO>> GetAllListings();
        public Task<bool> DeleteListing(int listingId);
        public Task<GetListingDTO> CreateListing(int userId, CreateListingDTO listingDTO, ListingStatus listingStatus,
                                                        Transmission transmission, BodyType bodyType, EngineType engineType); 
        public Task<GetListingDTO> UpdateListing(int userId, CreateListingDTO listingDTO, ListingStatus listingStatus,
                                                        Transmission transmission, BodyType bodyType, EngineType engineTyp);
    }
}