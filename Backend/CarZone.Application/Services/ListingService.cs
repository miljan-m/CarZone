using AutoMapper;
using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.Exceptions.ListingExceptions;
using CarZone.Application.Exceptions.UserExceptions;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Application.Validation.CreateValidation;
using CarZone.Application.Validation.UpdateValidation;
using CarZone.Domain.Enums;
using CarZone.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class ListingService : IListingService
{
    protected readonly IListingRepository _repository;
    protected readonly IUserRepository _userRepository;
    protected readonly IModelRepository _modelRepository;

    protected readonly IMapper _mapper;
    public ListingService(IListingRepository repository, IMapper mapper, IModelRepository modelRepository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _mapper = mapper;
        _modelRepository = modelRepository;
    }

    public async Task<GetListingDTO> GetListingById(int listingId)
    {
        var listing = await _repository.GetById(listingId) ?? throw new ListingNotFoundException(listingId.ToString(), StatusCodes.Status404NotFound);
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
            throw new ListingValidationException("Validation exception - Listing data is invalid", StatusCodes.Status422UnprocessableEntity);
        }
        var listing = _mapper.Map<Listing>(listingDTO);

        var images = imageURL.Select(url => new Image { ImageUrl = url }).ToList();
        listing.Images = images;
        var user = await _userRepository.GetById(userId) ?? throw new UserNotFoundException(userId.ToString(), StatusCodes.Status404NotFound);
        var createdListing = await _repository.Create(listing, userId);
        return _mapper.Map<GetListingDTO>(createdListing);
    }

    public async Task<bool> DeleteListing(int listingId)
    {
        var isDeleted = await _repository.Delete(listingId);
        if (!isDeleted) throw new ListingNotFoundException(listingId.ToString(), StatusCodes.Status404NotFound);
        return isDeleted;
    }

    public async Task<GetListingDTO> UpdateListing(int listingId, UpdateListingDTO listingDTO)
    {


        var validator = new UpdateListingDTOValidator();
        var result = validator.Validate(listingDTO);
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Validation eerror: {error.ErrorMessage}");
            }
            throw new ListingValidationException("Validation exception - Listing data is invalid", StatusCodes.Status422UnprocessableEntity);
        }

        var listing = await _repository.GetById(listingId);
        if (listing == null) throw new ListingNotFoundException(listingId.ToString(), StatusCodes.Status404NotFound);
        listing.AdditionalDescription = listingDTO.AdditionalDescription;
        listing.BodyType = listingDTO.BodyType;
        listing.EngineType = listingDTO.EngineType;
        listing.FuelConsuption = listingDTO.FuelConsuption;
        listing.ListingStatus = listingDTO.ListingStatus;
        listing.Mileage = listingDTO.Mileage;
        listing.Transmission = listingDTO.Transmission;
        listing.Price = listingDTO.Price;
        listing.ProductionYear = listingDTO.ProductionYear;


        await _repository.Update(listingId, listing);
        return null;
    }
}