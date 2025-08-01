using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PATCHUB.AuthServer.Application.Dtos.ClientCredential;
using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.AuthServer.Infrastructure.AuthTokenService;
using PATCHUB.AuthServer.Persistence.Repositories;
using PATCHUB.SharedLibrary.Dtos;


namespace PATCHUB.AuthServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        private readonly IClientCredentialService _service;
        public ClientController(ILogger<ClientController> logger
            , IClientCredentialService service
                               )
        {
            _logger = logger;
            _service = service;
        }


        [HttpPost("CreateClientCredential")]
        public Response<bool> CreateClientCredential([FromBody] ClientCredentialCreate request)
        {
            // var result = _clientCredentialRepository.CreateClientCredentialAsync(request).Result;
            var result = _service.CreateClientCredentialAsync(request).Result;
            return result ? Response<bool>.Success(200) : Response<bool>.Fail("Kayıt oluşturulamadı!", 404, true);
        }
    }
}
