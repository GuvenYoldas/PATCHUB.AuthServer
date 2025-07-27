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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRefreshTokenRepository _userRefreshTokenRepository;

        private readonly AuthenticationService _authenticationService;
        private readonly UserRepository _userRepository;

        public UserController(ILogger<UserController> logger
                                   , UserRepository userRepository
                                   , UserRefreshTokenRepository userRefreshTokenRepository
                                   , AuthenticationService authenticationService)
        {
            _logger = logger;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }


        [HttpPost("CreateUser")]
        public Response<bool> CreateUser([FromBody] AppUserCreate request)
        {
            try
            {
                //var test0 = _userRefreshTokenRepository.GetCount();
                //var test1 = _userRefreshTokenRepository.GetAll();
                //var test2 = _userRefreshTokenRepository.Get(w => w.IDUser == 3).FirstOrDefault();

                ////_userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = 6, Token = "deneme ALP#", ExpirationDate = DateTime.Now});
                //_userRefreshTokenRepository.Delete(test2);
                var result = _userRepository.CreateUserAsync(request).Result;
                return result ? Response<bool>.Success(200) : Response<bool>.Fail("Kayıt oluşturulamadı!", 404, true);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
