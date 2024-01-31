using Microsoft.AspNetCore.Mvc;

namespace WebApplication.New.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TideForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "High", "Low", "King"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public TideForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTide")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(50, 99),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
