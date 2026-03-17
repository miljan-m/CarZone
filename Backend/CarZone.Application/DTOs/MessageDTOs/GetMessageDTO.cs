using CarZone.Application.DTOs.ListingDTOs;
using CarZone.Application.DTOs.UserDTOs;

namespace CarZone.Application.DTOs.MessageDTOs
{
    public class GetMessageDTO
    {
        public string MessageText { get; set; }

        public int SenderId { get; set; }
        public GetUserDTO Sender { get; set; }

        public int ReceiverId { get; set; }
        public GetUserDTO Receiver { get; set; }

        public int? ListingId { get; set; }
        public GetListingDTO? Listing { get; set; }

        public DateTime SentAt { get; set; }

    }
}