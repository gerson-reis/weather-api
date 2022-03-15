namespace weather_models
{
    public class ForecastDay
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int Temperature { get; private set; }
        public string TemperatureUnit { get; private set; }
        public string WindSpeed { get; private set; }
        public string DetailedForecastDay { get; private set; }

        public ForecastDay(DateTime startTime, DateTime endTime, int temperature, string temperatureUnit, string windSpeed, string detailedForecastDay)
        {
            StartTime = startTime;
            EndTime = endTime;
            Temperature = temperature;
            TemperatureUnit = temperatureUnit;
            WindSpeed = windSpeed;
            DetailedForecastDay = detailedForecastDay;
        }
    }
}
