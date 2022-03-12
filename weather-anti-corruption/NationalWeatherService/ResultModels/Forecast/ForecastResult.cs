namespace weather_anti_corruption.NationalWeatherService.ResultModels.Forecast
{
    internal class ForecastResult
    {
        public List<object> Context { get; set; }
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public PropertiesForecast Properties { get; set; }
    }
}
