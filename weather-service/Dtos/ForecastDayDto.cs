namespace weather_application.Dtos
{
    public class ForecastDayDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public string WindSpeed { get; set; }
        public string DetailedForecastDay { get; set; }

        public ForecastDayDto(DateTime startTime, DateTime endTime, int temperature, string temperatureUnit, string windSpeed, string detailedForecastDay)
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
