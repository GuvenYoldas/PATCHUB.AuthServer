using Microsoft.AspNetCore.Mvc;
using PATCHUB.AuthServer.Application.Dtos;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using PATCHUB.AuthServer.Persistence.Repositories;
using PATCHUB.AuthServer.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.SharedLibrary.Dtos;

namespace PATCHUB.AuthServer.API.Controllers
{
    [EnableCors("AllowAllFrontend")]
    [ApiController]
    [Route("api/[controller]")]
    public class HelperController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;
        private readonly IContactRequestService _service;

        public HelperController(ILogger<ClientController> logger, IContactRequestService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("MailSender")]
        public async Task<Response<bool>> MailSender([FromBody] MailRequest request)
        {
            return await _service.MailSender(request.fullName, request.recipientEmail, request.messageText, request.location);
        }


    }
    public class MailRequest
    {
        public string recipientEmail { get; set; }
        public string fullName { get; set; }
        public string messageText { get; set; }
        public string location { get; set; }
    }

}
