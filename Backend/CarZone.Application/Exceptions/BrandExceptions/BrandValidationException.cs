using Microsoft.AspNetCore.Http;

namespace CarZone.Application.Exceptions.BrandExceptions
{
    public class BrandValidationException : CustomException
    {
        public int StatusCode { get; set; }
        public BrandValidationException(string validationMessage, int statusCode) : base($"{validationMessage} . Status code: ${statusCode}", statusCode)
        {
            StatusCode = statusCode;
        }
    }
}