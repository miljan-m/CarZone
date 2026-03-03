using Microsoft.AspNetCore.Http;

namespace CarZone.Application.Exceptions.BrandExceptions
{
    public class BrandNotFoundException : CustomException
    {
        public int StatusCode { get; set; }
        public BrandNotFoundException(string brandId, int statusCode) : base($"Brand with BrandId/Name={brandId} does not exist. Status code: ${statusCode}", statusCode)
        {
            StatusCode = statusCode;
        }
    }
}