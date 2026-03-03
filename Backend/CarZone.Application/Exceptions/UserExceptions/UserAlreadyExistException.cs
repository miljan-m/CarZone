namespace CarZone.Application.Exceptions.UserExceptions
{
    public class UserAlreadyExistException : CustomException
    {
        public int StatusCode { get; set; }
        public UserAlreadyExistException(int statusCode) : base($"User with provided atributes already exists. Status code: ${statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}