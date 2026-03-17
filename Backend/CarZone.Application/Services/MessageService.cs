using AutoMapper;
using CarZone.Application.DTOs.MessageDTOs;
using CarZone.Application.Interfaces.Repositories;
using CarZone.Application.Interfaces.ServiceInterfaces;
using CarZone.Domain.Models;

namespace CarZone.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;
        public MessageService(IMessageRepository repository, IUserRepository userRepository, IListingRepository listingRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _listingRepository = listingRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetMessageDTO>> GetAllMessages()
        {
            var messages = await _repository.GetAllMessages();
            var mappedMessages = messages.Select(m => _mapper.Map<GetMessageDTO>(m));
            return mappedMessages;
        }

        public async Task<bool> SaveMessage(SaveMessageDTO message)
        {
            Console.WriteLine(message.ReceiverId);
            Console.WriteLine(message.MessageText);
            Console.WriteLine(message.SenderId);
            Console.WriteLine(message.SentAt);
            Console.WriteLine(message.ListingId);

            var messageEntity = new Message
            {
                ListingId = message.ListingId,
                MessageText = message.MessageText,
                ReceiverId = message.ReceiverId,
                SenderId = message.SenderId,
                Sender = await _userRepository.GetById(message.SenderId),
                Receiver = await _userRepository.GetById(message.ReceiverId)
            };

            return await _repository.SaveMessage(messageEntity);
        }
    }
}