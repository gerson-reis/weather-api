using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace weather_infrastructure.Exceptions
{
    public abstract class InternalExceptionBase : ApplicationException
    {
        public InternalExceptionBase(EventId EventId, string? message) : base(message)
        {
        }
        public EventId EventId { get; set; }
    }
}
