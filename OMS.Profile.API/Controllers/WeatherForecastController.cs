using KafkaStorm.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OMS.Profile.Application.Common.IntegrationEvents.Events;

namespace OMS.Profile.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IProducer _producer;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IProducer producer)
    {
        _logger = logger;
        _producer = producer;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        await Task.WhenAll(_producer.ProduceAsync(new HelloEvent(DateTime.Now)),
            _producer.ProduceAsync(new ByeEvent(DateTime.Now)));
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}