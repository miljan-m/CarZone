namespace CarZone.Application.Exceptions.ModelExceptions
{
    public class ModelValidationException : CustomException
    {
        public int StatusCode { get; set; }

        public ModelValidationException(string validationMessage, int statusCode) : base($"{validationMessage} . Status code: ${statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}