using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using WebApplication1.Controllers;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<WeatherForecastControllerBenchmark>();
    }
}

[MemoryDiagnoser]
public class WeatherForecastControllerBenchmark
{
    [Benchmark]
    public void CreateWeatherForecast()
    {
        var controller = new WeatherForecastController(null);
        var result = controller.Get();
    }
}
