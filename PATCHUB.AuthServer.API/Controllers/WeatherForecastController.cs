using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PATCHUB.AuthServer.Application.Dtos;
using PATCHUB.AuthServer.Infrastructure.AuthTokenService;
using PATCHUB.AuthServer.Persistence.Repositories;
using PATCHUB.SharedLibrary.Dtos;
namespace PATCHUB.AuthServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserRefreshTokenRepository _userRefreshTokenRepository;

        private readonly AuthenticationService _authenticationService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
                                       , UserRefreshTokenRepository userRefreshTokenRepository
                                       , AuthenticationService authenticationService)
        {
            _logger = logger;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _authenticationService = authenticationService;
        }

        [HttpGet("CreateTokenAsync")]
        public Response<AppToken> CreateTokenAsync()
        {
            try
            {
                //var test0 = _userRefreshTokenRepository.GetCount();
                //var test1 = _userRefreshTokenRepository.GetAll();
                //var test2 = _userRefreshTokenRepository.Get(w => w.IDUser == 3).FirstOrDefault();

                ////_userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = 6, Token = "deneme ALP#", ExpirationDate = DateTime.Now});
                //_userRefreshTokenRepository.Delete(test2);

               return _authenticationService.CreateTokenAsync(new Application.Dtos.AppLogin { Email = "guvenyoldas@gmail.com", Password = "ss" }).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpGet("CreateTokenByRefreshToken")]
        public Response<AppToken> CreateTokenByRefreshToken(string refreshToken)
        {
            try
            {
                //var test0 = _userRefreshTokenRepository.GetCount();
                //var test1 = _userRefreshTokenRepository.GetAll();
                //var test2 = _userRefreshTokenRepository.Get(w => w.IDUser == 3).FirstOrDefault();

                ////_userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = 6, Token = "deneme ALP#", ExpirationDate = DateTime.Now});
                //_userRefreshTokenRepository.Delete(test2);

                    return _authenticationService.CreateTokenByRefreshToken(refreshToken).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
