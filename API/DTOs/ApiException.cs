namespace API.DTOs
{
    public class ApiException
    {
        public short StatusCode { get; private set; }
        public string Message { get; private set; }
        public string? Details { get; private set; }

        public ApiException(short statusCode, string message, string? details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
