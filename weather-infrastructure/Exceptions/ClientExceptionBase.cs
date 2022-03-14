using Microsoft.AspNetCore.Http;

namespace weather_infrastructure.Exceptions
{
    public abstract class ClientExceptionBase : ApplicationException
    {
        public ClientExceptionBase(int httpStatusCode, string? message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
        public int HttpStatusCode { get; private set; }
    }
}
