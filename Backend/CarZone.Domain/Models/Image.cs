namespace CarZone.Domain.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }

        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public Image()
        {
        }
    }
}