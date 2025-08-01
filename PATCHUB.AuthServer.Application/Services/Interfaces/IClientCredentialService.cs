using PATCHUB.AuthServer.Application.Dtos.ClientCredential;

namespace PATCHUB.AuthServer.Application.Services.Interfaces
{
    public interface IClientCredentialService
    {
        Task<bool> CreateClientCredentialAsync(ClientCredentialCreate input);
    }
}
