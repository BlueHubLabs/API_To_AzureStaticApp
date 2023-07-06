using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Models;
using API.DataAccess.Repository;
using API.Services.Services;
using API.Services.Interfaces;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        protected readonly IStateCityService StateCityService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStateCityService stateCityService)
        {
            _logger = logger;
            StateCityService = stateCityService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("GetStateCityDetails")]
        public async Task<ActionResult<IReadOnlyList<StateCityDetails>>> GetCityStateDetailsAsync([FromQuery] int CountryMasterID)
        {
            _logger.LogInformation("Accessing {} action {} for CountryMasterID {}", nameof(WeatherForecastController), nameof(GetCityStateDetailsAsync), CountryMasterID);
            var cityStateDetails = await StateCityService.GetCityStateDetailsAsync(CountryMasterID);
            if (cityStateDetails != null && cityStateDetails.Count > 0)
            {
                return cityStateDetails.ToList();



            }
            else
            {
                return NotFound("State City Details not exists for Country : " + CountryMasterID);
            }
        }
    }
}