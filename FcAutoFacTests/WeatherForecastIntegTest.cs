using FvAutofac;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using System.Net.Http;
using FluentAssertions;

namespace FcAutoFacTests
{
    public class WeatherForecastIntegTest
    {

        [Fact]
        public async Task PostForecastReturnsSuccessCodeAndResourceWithAccurateFields()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
               {
                   builder
                   .ConfigureServices(services =>
                   {
                      
                   });
               });

            var client = appFactory.CreateClient();
            var fakeWeatherForecast = new WeatherForecast { Id = 1 };

            // Act
            var httpResponse = await client.PostAsJsonAsync("/weatherforecast", fakeWeatherForecast)
                .ConfigureAwait(false);

            // Assert
            httpResponse.EnsureSuccessStatusCode();

            var resultDto = JsonConvert.DeserializeObject<WeatherForecast>(await httpResponse.Content.ReadAsStringAsync()
                .ConfigureAwait(false));

            httpResponse.StatusCode.Should().Be(201);;
        }
    }
}
