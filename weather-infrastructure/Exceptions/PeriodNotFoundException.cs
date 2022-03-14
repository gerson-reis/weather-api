using Microsoft.Extensions.Logging;

namespace weather_infrastructure.Exceptions
{
    public class PropertiesNotFoundException : InternalExceptionBase
    {
        public PropertiesNotFoundException() : base(new EventId(10, "PropertiesNotFound"), "Could not find the properties to get the periods")
        {
        }
    }
}
