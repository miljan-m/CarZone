using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly CarZoneDBContext _context;
        private readonly DbSet<Image> _dbset;
        public ImageRepository(CarZoneDBContext context)
        {
            _context = context;
            _dbset = _context.Set<Image>();
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetImageByListingId(int listingId)
        {
            var images=await _dbset.Where(i=>i.ListingId==listingId).ToListAsync();
            if(images==null) return null;
            return images;
        }
    }
}