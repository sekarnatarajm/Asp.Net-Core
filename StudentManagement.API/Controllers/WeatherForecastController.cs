using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Exceptions;
using StudentManagement.API.Filters;

namespace StudentManagement.API.Controllers
{
    [CustomRoleAuthorize("Manager")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> _logger) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Received request WeatherForecast");
            string firstName = null;
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ValidationException("Validation failed for the request");
            }
            string fullName = firstName.ToString();
            _logger.LogInformation("Executed request WeatherForecast");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
