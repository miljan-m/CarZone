using CarZone.Application.Interfaces.Repositories;
using CarZone.Domain.Models;
using CarZone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarZone.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        public CarZoneDBContext _dbContext;
        public DbSet<Message> _dbSet;

        public MessageRepository(CarZoneDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Message>();
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _dbSet.Include(m => m.Sender)
                                .Include(m => m.Receiver)
                                .Include(m => m.Listing)
                                .ThenInclude(l => l.Images)
                                .Include(m => m.Listing)
                                .ThenInclude(l => l.Model)
                                .ThenInclude(m => m.Brand)
                                .ToListAsync();
        }

        public async Task<bool> SaveMessage(Message message)
        {
            await _dbSet.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}