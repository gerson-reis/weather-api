using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Reflection;

namespace weather.Integrations.Tests
{
    [SetUpFixture]
    internal class BaseSetup
    {
        public static IWebDriver driver;// = new FirefoxDriver();

        [OneTimeSetUp]
        public void Setup()
        {
            //ChromeOptions options = new ChromeOptions();
            ////options.AddArgument("--headless");
            //options.AddArgument("--start-maximized");

            //IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            ////driver.Url = "https://localhost:7167/";

            ////driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(5));

            //driver.Navigate().GoToUrl("https://localhost:7167/api/check");
            /* Code to visit site URL and Login */

            //- I assume Global Exception handling code will go here as i have globally defined the implicit wait time-//
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
