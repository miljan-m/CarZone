namespace CarZone.Application.Exceptions.ListingExceptions
{
    public class ListingNotFoundException : CustomException
    {
        public int StatusCode { get; set; }
        public ListingNotFoundException(string listingId, int statusCode) : base($"Listing with listingId={listingId} does not exist. Status code: {statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}