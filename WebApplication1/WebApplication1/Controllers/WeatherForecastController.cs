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
        var weatherForecastsResult = WeatherForecastsSuccessResult();
        return weatherForecastsResult.ToActionResult();
    }
    
    private static Result<IEnumerable<WeatherForecast>> WeatherForecastsSuccessResult()
    {
        return SuccessResult<IEnumerable<WeatherForecast>>.Create(
            (SourceId: "WeatherForecastController", StatusCode: HttpStatusCode.OK, Value: Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray()));
    }
    
    private static Result<IEnumerable<WeatherForecast>> WeatherForecastsProblemDetailsTupleResult()
    {
        return ProblemDetailsResult<IEnumerable<WeatherForecast>>.Create(
            (SourceId: "WeatherForecastController", StatusCode: HttpStatusCode.InternalServerError, ProblemDetails: new ProblemDetails
            {
                Title = "An unexpected error occurred.",
                Detail = "An unexpected error occurred while processing the request.",
                Status = (int)HttpStatusCode.InternalServerError
            }));
    }
}
