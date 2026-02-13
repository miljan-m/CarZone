using AutoMapper;
using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Validation.CreateValidation;
using CarZone.Application.Validation.UpdateValidation;
using CarZone.Domain.Enums;
using CarZone.Domain.Models;
using FluentValidation;

public class ListingService : IListingService
{
    protected readonly IListingRepository _repository;
    protected readonly IModelRepository _modelRepository;

    protected readonly IMapper _mapper;
    public ListingService(IListingRepository repository, IMapper mapper, IModelRepository modelRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _modelRepository = modelRepository;
    }

    public async Task<GetListingDTO> GetListingById(int listingId)
    {
        var listing = await _repository.GetById(listingId);
        if (listing == null) return null;
        return _mapper.Map<GetListingDTO>(listing);
    }
    public async Task<IEnumerable<GetListingDTO>> GetAllListings()
    {
        var listing = await _repository.GetAll();

        if (listing.Any())
        {
            return listing.Select(l => _mapper.Map<GetListingDTO>(l));
        }

        return null;
    }
    public async Task<GetListingDTO> CreateListing(int userId, CreateListingDTO listingDTO, List<string> imageURL)
    {
        var validator = new CreateListingDTOValidator();
        var result = validator.Validate(listingDTO);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Validation error: {error.ErrorMessage}");
            }
            throw new ValidationException("Listing data is invalid");
        }
        var listing = _mapper.Map<Listing>(listingDTO);

        var images = imageURL.Select(url => new Image { ImageUrl = url }).ToList();
        listing.Images = images;
        var createdListing = await _repository.Create(listing, userId);
        return _mapper.Map<GetListingDTO>(createdListing);
    }

    public async Task<bool> DeleteListing(int listingId)
    {
        return await _repository.Delete(listingId);
    }

    public async Task<GetListingDTO> UpdateListing(int userId, UpdateListingDTO listingDTO, ListingStatus listingStatus,
                                                        Transmission transmission, BodyType bodyType, EngineType engineType)
    {


        var validator = new UpdateListingDTOValidator();
        var result = validator.Validate(listingDTO);
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Validation eerror: {error.ErrorMessage}");
            }
            throw new ValidationException("Listing data is invalid");
        }

        var listing = await _repository.GetById(userId);
        listing.AdditionalDescription = listingDTO.AdditionalDescription;
        listing.BodyType = bodyType;
        listing.EngineType = engineType;
        listing.FuelConsuption = listingDTO.FuelConsuption;
        listing.ListingStatus = listingStatus;
        listing.Mileage = listingDTO.Mileage;
        listing.Transmission = transmission;
        listing.Price = listingDTO.Price;
        listing.ProductionYear = listingDTO.ProductionYear;
        listing.ModelId = listingDTO.ModelId;


        await _repository.Update(userId, listing);
        return null;
    }
}