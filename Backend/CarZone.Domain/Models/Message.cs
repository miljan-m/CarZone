using System.ComponentModel.DataAnnotations;

namespace CarZone.Domain.Models
{
    public class Message
    {


        [Key]
        public int MessageId { get; set; }
        public string MessageText { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public int? ListingId { get; set; }
        public Listing? Listing { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

        public Message()
        {
        }

        public Message(string messageText, int senderId, int receiverId)
        {
            MessageText = messageText;
            SenderId = senderId;
            ReceiverId = receiverId;
            SentAt = DateTime.Now;
        }



    }
}