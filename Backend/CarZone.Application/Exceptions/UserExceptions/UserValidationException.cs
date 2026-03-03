namespace CarZone.Application.Exceptions.UserExceptions
{
    public class UserValidationException : CustomException
    {
        public int StatusCode { get; set; }
        public UserValidationException(string validationMessage, int statusCode) : base($"{validationMessage}. Status code: {statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}