namespace CarZone.Application.Exceptions.ListingExceptions
{
    public class ListingValidationException : CustomException
    {
        public int StatusCode { get; set; }
        public ListingValidationException(string message, int statusCode) : base($"{message}. Status code: {statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}