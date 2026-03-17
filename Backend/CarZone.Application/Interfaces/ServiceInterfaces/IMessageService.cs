using CarZone.Application.DTOs.MessageDTOs;
using CarZone.Domain.Models;

namespace CarZone.Application.Interfaces.ServiceInterfaces
{
    public interface IMessageService
    {

        public Task<IEnumerable<GetMessageDTO>> GetAllMessages();

        public Task<bool> SaveMessage(SaveMessageDTO message);

    }
}