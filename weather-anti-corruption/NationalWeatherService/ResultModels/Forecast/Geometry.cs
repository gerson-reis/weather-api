namespace weather_anti_corruption.NationalWeatherService.ResultModels.Forecast
{
    internal class Geometry
    {
        public string Type { get; set; }
        public List<List<List<double>>> Coordinates { get; set; }
    }

}
