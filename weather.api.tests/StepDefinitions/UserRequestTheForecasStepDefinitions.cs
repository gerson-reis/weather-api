using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

namespace weather.Integrations.Tests.StepDefinitions
{
    [Binding]
    public class UserRequestTheForecasStepDefinitions
    {
        string address = "";

        [Given(@"the user enter int index page")]
        public void GivenTheUserEnterIntIndexPage() {}

        [Given(@"write the address '([^']*)'")]
        public void GivenWriteTheAddress(string p0)
        {
            address = p0;
        }

        [When(@"the user submit the address")]
        public void WhenTheUserSubmitTheAddress()
        {
            var client = new HttpClient();

            var asd = client
                .GetAsync("https://localhost:7167/get-forecast-from?address=4600 Silver Hill Rd, Washington, DC 20233").Result;

            //   var request = new HttpRequestWrapper()
            //                .SetMethod(Method.Get)
            //                .SetResourse("/api/get-forecast-from?address=4600 Silver Hill Rd, Washington, DC 20233")
            //                .AddParameter("address", address);

            //var _restResponse = new RestResponse();
            //_restResponse = request.Execute();

            //Assert.Equals(HttpStatusCode.OK, _restResponse.StatusCode);

            //var result = JsonConvert.DeserializeObject<List<Period>>(_restResponse.Content);

            //Assert.IsNotNull(result);
        }

        [Then(@"the forecast endpoint is called and return the result")]
        public void ThenTheForecastEndpointIsCalledAndReturnTheResult()
        {
        }
    }
}
