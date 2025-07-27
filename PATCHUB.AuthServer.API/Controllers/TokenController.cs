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
    public class TokenController : ControllerBase
    {

        private readonly ILogger<TokenController> _logger;
        private readonly UserRefreshTokenRepository _userRefreshTokenRepository;

        private readonly AuthenticationService _authenticationService;
        private readonly UserRepository _userRepository;

        public TokenController(ILogger<TokenController> logger
                               , UserRepository userRepository
                               , UserRefreshTokenRepository userRefreshTokenRepository
                               , AuthenticationService authenticationService)
        {
            _logger = logger;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        [HttpPost("CreateTokenAsync")]
        public Response<AppToken> CreateTokenAsync([FromBody] AppLogin request)
        {
            try
            {

                var clientId = Request.Headers["Client-Id"].FirstOrDefault();
                var clientSecret = Request.Headers["Client-Secret"].FirstOrDefault();

                //var test0 = _userRefreshTokenRepository.GetCount();
                //var test1 = _userRefreshTokenRepository.GetAll();
                //var test2 = _userRefreshTokenRepository.Get(w => w.IDUser == 3).FirstOrDefault();

                ////_userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = 6, Token = "deneme ALP#", ExpirationDate = DateTime.Now});
                //_userRefreshTokenRepository.Delete(test2);

                return _authenticationService.CreateTokenAsync(new Application.Dtos.AppLogin { Email = "alp.yoldas@gmail.com", Password = "1q2w3e4r5t6y7u8ı9o_!#" }).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }


        [Authorize]
        [HttpGet("CreateTokenByRefreshToken")]
        public Response<AppToken> CreateTokenByRefreshToken([FromBody] AppUserCreate request)
        {
            try
            {
                //var test0 = _userRefreshTokenRepository.GetCount();
                //var test1 = _userRefreshTokenRepository.GetAll();
                //var test2 = _userRefreshTokenRepository.Get(w => w.IDUser == 3).FirstOrDefault();

                ////_userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = 6, Token = "deneme ALP#", ExpirationDate = DateTime.Now});
                //_userRefreshTokenRepository.Delete(test2);
                var user = HttpContext.User;
                // veya
                var token = Request.Headers["Authorization"];

                return _authenticationService.CreateTokenByRefreshToken("iwPzZtFqxrn0bRCw1gn5Rv01v5Dl934jzxnjw3XlfFo=").Result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
