using Microsoft.AspNetCore.Mvc;
using PATCHUB.AuthServer.Application.Dtos;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using PATCHUB.AuthServer.Persistence.Repositories;
using PATCHUB.AuthServer.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;

namespace PATCHUB.AuthServer.API.Controllers
{
    [EnableCors("AllowAllFrontend")]
    [ApiController]
    [Route("api/[controller]")]
    public class HelperController : ControllerBase
    {
        private readonly ContactRequestRepository _contactRequestRepository;
        public HelperController( ContactRequestRepository contactRequestRepository)
        {
            _contactRequestRepository = contactRequestRepository;
        }
      
        [HttpPost("MailSender")]
        public string MailSender([FromBody]MailRequest request)
        {
            Console.WriteLine($"Received: {JsonSerializer.Serialize(request)}");
            try
            {
                if (string.IsNullOrEmpty(request.recipientEmail))
                {
                    return "Lütfen iletişim bilgisi belirtiniz!";
                }
                _contactRequestRepository.Insert(new ContactRequestEntity
                {
                    FullName = request.fullName,
                    Mail = request.recipientEmail,
                    MessageText = request.messageText,
                    Location = request.location
                });
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }

            return "OK!";
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
