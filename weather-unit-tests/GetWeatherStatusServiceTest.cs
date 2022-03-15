using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using weather_anti_corruption.Geocoding;
using weather_anti_corruption.Geocoding.ResultModels;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;
using weather_application.Services;
using weather_infrastructure.CacheServices;
using weather_models;
using Xunit;

namespace weather_unit_tests
{
    public class GetWeatherStatusServiceTest
    {
        private Mock<IGeocodingRestService> geocodingRestService = new Mock<IGeocodingRestService>();
        private Mock<INationalWeatherRestService> weatherRestService = new Mock<INationalWeatherRestService>();
        private Mock<ICacheService> cacheService = new Mock<ICacheService>();

        private GetWeatherStatusService service;
        public GetWeatherStatusServiceTest()
        {
            service = new GetWeatherStatusService(geocodingRestService.Object, weatherRestService.Object, cacheService.Object);
        }

        string validAdressToTest = "address=4600 Silver Hill Rd, Washington, DC 20233";
        CoordinatesModel coordinatesModel = new CoordinatesModel { Latitude = "76.92744", Longitude = "38.845985" };
        IList<Period> listPeriod = new List<Period>() { new Period()};

        [Fact]
        public void VerifyIfWeatherServiceIsCalled()
        {
            geocodingRestService.Setup(x => x.Get(validAdressToTest)).Returns(Task.FromResult(coordinatesModel));
            weatherRestService.Setup(x => x.Get(coordinatesModel.Latitude, coordinatesModel.Longitude)).Returns(Task.FromResult(listPeriod));

            service.GetForecastByAddress(validAdressToTest).Wait();

            weatherRestService.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyIfGeocodingIsCalled()
        {
            geocodingRestService.Setup(x => x.Get(validAdressToTest)).Returns(Task.FromResult(coordinatesModel));
            weatherRestService.Setup(x => x.Get(coordinatesModel.Latitude, coordinatesModel.Longitude)).Returns(Task.FromResult(listPeriod));

            service.GetForecastByAddress(validAdressToTest).Wait();

            geocodingRestService.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void TestIfCacheIsWorking()
        {
            geocodingRestService.Setup(x => x.Get(validAdressToTest)).Returns(Task.FromResult(coordinatesModel));
            weatherRestService.Setup(x => x.Get(coordinatesModel.Latitude, coordinatesModel.Longitude)).Returns(Task.FromResult(listPeriod));

            service.GetForecastByAddress(validAdressToTest).Wait();

            cacheService.Setup(x => x.Get<IList<Period>>(validAdressToTest)).Returns(Task.FromResult(listPeriod));
                        
            service.GetForecastByAddress(validAdressToTest).Wait();

            geocodingRestService.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            weatherRestService.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }        
    }
}
