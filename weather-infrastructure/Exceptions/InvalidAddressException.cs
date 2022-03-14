using Microsoft.AspNetCore.Http;

namespace weather_infrastructure.Exceptions
{
    public class InvalidAddressException : ClientExceptionBase
    {
        public InvalidAddressException() : base(StatusCodes.Status400BadRequest, "Invalid address")
        {
        }
    }
}
