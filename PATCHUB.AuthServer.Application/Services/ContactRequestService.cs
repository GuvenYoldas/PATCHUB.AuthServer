using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Application.Dtos.ClientCredential;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.SharedLibrary.Dtos;
using PATCHUB.SharedLibrary.ErrorHandling.Exceptions;
using PATCHUB.AuthServer.Application.Dtos;

namespace PATCHUB.AuthServer.Application.Services
{
    public class ContactRequestService : IContactRequestService
    {
        private readonly IContactRequestRepository _contactRequestRepository;

        public ContactRequestService(
                IContactRequestRepository contactRequestRepository
                )
        {
            _contactRequestRepository = contactRequestRepository;
        }

        public async Task<Response<bool>> MailSender(string fullName, string mail, string messageText, string location)
        {
            if (string.IsNullOrEmpty(mail.Trim()))
            {
                throw new BadRequestException("Giriş Bilgileri boş!");
            }
            _contactRequestRepository.Insert(ContactRequestEntity.Create(

                fullName = fullName,
                mail = mail,
                messageText = messageText,
                location = location
            )) ;

            return Response<bool>.Success(true, 200);
        }
    }
}

