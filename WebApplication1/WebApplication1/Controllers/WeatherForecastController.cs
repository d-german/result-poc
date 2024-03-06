using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    private readonly ILogger<WeatherForecastController> _logger;
    
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        var weatherForecastsResult = WeatherForecastsResult();
        return weatherForecastsResult.ToActionResult();
    }
    
    private static Result<IEnumerable<WeatherForecast>> WeatherForecastsResult()
    {
        return new SuccessResult<IEnumerable<WeatherForecast>>
        {
            SourceId = "WeatherForecastController",
            //Context = nameof(WeatherForecastsResult),
            StatusCode = HttpStatusCode.OK,
            Value = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray()
        };
    }
}
