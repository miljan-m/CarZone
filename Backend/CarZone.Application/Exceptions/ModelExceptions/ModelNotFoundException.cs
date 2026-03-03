namespace CarZone.Application.Exceptions.ModelExceptions
{
    public class ModelNotFoundException : CustomException
    {
        public int StatusCode { get; set; }

        public ModelNotFoundException(string modelId, int statusCode) : base($"Model with ModelId/Name={modelId} does not exist. Status code: ${statusCode}",statusCode)
        {
            StatusCode = statusCode;
        }
    }
}