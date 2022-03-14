using Microsoft.Extensions.Logging;

namespace weather_infrastructure.Exceptions
{
    public class PeriodNotFoundException : InternalExceptionBase
    {
        public PeriodNotFoundException() : base(new EventId(10, "PeriodNotFound"), "Could not find geolocation")
        {
        }
    }
}
