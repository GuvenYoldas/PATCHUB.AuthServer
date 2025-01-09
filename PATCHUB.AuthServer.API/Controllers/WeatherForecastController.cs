using Microsoft.AspNetCore.Mvc;
using PATCHUB.AuthServer.Persistence.Repositories;

namespace PATCHUB.AuthServer.API.Controllers
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
        private readonly UserRefreshTokenRepository _userRefreshTokenRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
                                       , UserRefreshTokenRepository userRefreshTokenRepository)
        {
            _logger = logger;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var test0 = _userRefreshTokenRepository.GetCount();
            var test1 = _userRefreshTokenRepository.GetAll();
            var test2 = _userRefreshTokenRepository.Get(w => w.IDUser == 3);

            _userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = 6, Token = "deneme ALP#", ExpirationDate = DateTime.Now});
           
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
