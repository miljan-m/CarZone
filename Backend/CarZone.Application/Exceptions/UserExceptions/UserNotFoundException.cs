namespace CarZone.Application.Exceptions.UserExceptions
{
    public class UserNotFoundException : CustomException
    {
        public int StatusCode { get; set; }
        public UserNotFoundException(string userId, int statusCode) : base($"User with id={userId} does not exist. Status code: {statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}