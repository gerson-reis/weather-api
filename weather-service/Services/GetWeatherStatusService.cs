﻿using weather_anti_corruption.Geocoding;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;
using weather_application.IServices;
using weather_infrastructure.CacheServices;
using weather_models;

namespace weather_application.Services
{
    public partial class GetWeatherStatusService : IGetWeatherStatusService
    {
        private readonly IGeocodingRestService geocodingRestService;
        private readonly INationalWeatherRestService weatherRestService;
        private readonly ICacheService cacheService;

        public GetWeatherStatusService(IGeocodingRestService geocodingRestService, INationalWeatherRestService weatherRestService, ICacheService cacheService)
        {
            this.geocodingRestService = geocodingRestService;
            this.weatherRestService = weatherRestService;
            this.cacheService = cacheService;
        }

        public async Task<IList<ForecastDay>>? GetForecastByAddress(string address)
        {
            var inCache = await cacheService.Get<IList<ForecastDay>>(address);

            if (inCache != null)
                return inCache;

            var coordinates = await geocodingRestService.Get(address);

            _ = coordinates ?? throw new ArgumentNullException(nameof(address));

            var periods = await weatherRestService.Get(coordinates.Latitude, coordinates.Longitude);

            if (!periods.Any())
                return null;

            var result = GetResult(periods);

            await cacheService.Set<IList<ForecastDay>>(address, result, DateTime.Now.AddMinutes(10));

            return result;
        }

        private IList<ForecastDay> GetResult(IList<Period> periods) =>
                                periods.Select(x => new ForecastDay(x.StartTime, x.EndTime, x.Temperature, x.TemperatureUnit, x.WindSpeed, x.DetailedForecast)).ToList();
    }
}
