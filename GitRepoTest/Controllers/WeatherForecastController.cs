using GitRepoTest.Helpers.Attributes;
using GitRepoTest.Helpers.Filters;
using Grpc.Net.Client;
using GitRepoTest.gRPC;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace GitRepoTest.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {

            

            var value = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogError("GetWeatherForecast return value : {value}, {asad}", value,
            Directory.GetCurrentDirectory());

            return value;
        }

        [HttpGet]
        [Route("/CheckAttributes/")]
        // [TypeFilter<CustomResultFilter>]

        [CustomValidation]
        [TypeFilter<CustomActionFilter>]
        [ServiceFilter<CustomResultFilter>]
        public IActionResult TestAttributesandFilters()
        {


            return Ok();
        }

        [HttpHead]
        [Route("/RemoveControllerRoute")]
        [Route("Home/Index/{id:regex(\\d?\\w.)?}/dududu")]
        public IActionResult TestRoutes(string? id)
        {

            _logger.LogInformation("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

           var result =  HttpContext.RequestServices.GetRequiredService<SingletonClassTest>();
           // HttpContext.Features.GetRequiredFeature<WeatherForecast>();

            return Ok(id);
        }


        [HttpPost]
        [Route("/FromBody")]
        public IActionResult TestRoutes([FromBody] string? id, int name)
        {

            _logger.LogInformation("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


            using var channel = GrpcChannel.ForAddress("https://localhost:7047");

            var testchannel = new TestgRPC.TestgRPCClient(channel);

          //  channel.TestgRPC

            return Ok(id);
        }
    }
}
