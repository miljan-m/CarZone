namespace CarZone.Application.Exceptions.ModelExceptions
{
    public class ModelAlreadyExistException : CustomException
    {
        public int StatusCode { get; set; }

        public ModelAlreadyExistException(string modelId, int statusCode) : base($"Model with ModelId/Name={modelId} already exist. Status code: ${statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}