namespace weather_anti_corruption.NationalWeatherService.ResultModels.Forecast
{
    internal class PropertiesForecast
    {
        public DateTime Updated { get; set; }
        public string Units { get; set; }
        public string ForecastGenerator { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime UpdateTime { get; set; }
        public string ValidTimes { get; set; }
        public Elevation Elevation { get; set; }
        public List<Period> Periods { get; set; }
    }

}
