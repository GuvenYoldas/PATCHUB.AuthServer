
using PATCHUB.SharedLibrary.Dtos;

namespace PATCHUB.AuthServer.Application.Services.Interfaces
{
    public interface IContactRequestService
    {
        Task<Response<bool>> MailSender(string fullName, string mail, string messageText, string location);
    }
}
