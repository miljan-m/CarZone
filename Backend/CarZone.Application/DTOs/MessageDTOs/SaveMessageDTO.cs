using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.DTOs.UserDTOs;

namespace CarZone.Application.DTOs.MessageDTOs
{
    public class SaveMessageDTO
    {
        public string MessageText { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public int? ListingId { get; set; }

        public DateTime SentAt { get; set; }
    }
}