using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.Repositories
{
    public interface IMessageRepository
    {

        public Task<bool> SaveMessage(Message message);
        public Task<IEnumerable<Message>> GetAllMessages();

    }
}