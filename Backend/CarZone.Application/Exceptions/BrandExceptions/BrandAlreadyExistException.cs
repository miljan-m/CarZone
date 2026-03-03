using Microsoft.AspNetCore.Http;

namespace CarZone.Application.Exceptions.BrandExceptions
{
    public class BrandAlreadyExistException : CustomException
    {
        public int StatusCode { get; set; }
        public BrandAlreadyExistException(string brandName, int statusCode) : base($"Brand with BrandName={brandName} already exists. Status code: ${statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}