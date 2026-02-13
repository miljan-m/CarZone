using CarZone.Application.DTOs.ModelDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {
        protected readonly CarZoneDBContext _context;
        protected readonly DbSet<Listing> _dbSet;

        protected readonly CarZoneDBContext _contextUser;
        protected readonly DbSet<User> _dbSetUser;

        public ListingRepository(CarZoneDBContext context, CarZoneDBContext contextUser)
        {
            _context = context;
            _dbSet = _context.Set<Listing>();

            _contextUser = contextUser;
            _dbSetUser = contextUser.Set<User>();
        }



        public async Task<Listing> GetById(int id)
        {
            var listing = await _dbSet
                                .Include(l => l.User)
                                .Include(l => l.Buyer)
                                .FirstAsync(l => l.ListingID == id);
            return listing;
        }
        public async Task<IEnumerable<Listing>> GetAll()
        {
            var listings = await _dbSet
                                .Include(l => l.User)
                                .Include(l => l.Buyer)
                                .Include(l=>l.Images)
                                .Include(l => l.Model).ThenInclude(m=>m.Brand)
                                .ToListAsync();
            return listings;
        }
        public async Task<Listing> Create(Listing obj, int id = int.MinValue)
        {
            var user = await _dbSetUser.FindAsync(id);
            if (user == null) return null;
            obj.UserId = id;
            obj.User = user;
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var listing = await _dbSet.FindAsync(id);
            if (listing == null) return false;
            _dbSet.Remove(listing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, Listing obj)
        {
            var listing = await _dbSet.FindAsync(id);
            if (listing == null) return false;

            var properties = _dbSet.Entry(listing).Properties;
            foreach (var prop in properties)
            {
                if (prop.Metadata.IsPrimaryKey()) continue;
                if (prop.Metadata.IsForeignKey()) continue;

                var propInfo = typeof(Listing).GetProperty(prop.Metadata.Name);
                if (propInfo == null) continue;
                var newValue = propInfo.GetValue(obj);
                if (newValue == null) return false;
                prop.CurrentValue = newValue;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}