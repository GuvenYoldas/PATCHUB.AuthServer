using PATCHUB.AuthServer.Application.Dtos.ClientCredential;
using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Enumeration;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using PATCHUB.SharedLibrary.Helpers;

namespace PATCHUB.AuthServer.Application.Services
{
    public class ClientCredentialService : IClientCredentialService
    {
        private readonly IClientCredentialRepository _clientCredentialRepository;
        private readonly IClientRateLimitPolicyRepository _rateLimitPolicyRepository;
        private readonly IClientAllowedIpRepository _allowedIpRepository;

        private readonly IAuthUnitOfWork _unitOfWork;

        public ClientCredentialService(
            IClientCredentialRepository clientCredentialRepository,
            IClientRateLimitPolicyRepository rateLimitPolicyRepository,
            IClientAllowedIpRepository allowedIpRepository,
            IAuthUnitOfWork unitOfWork
            )
        {
            _clientCredentialRepository = clientCredentialRepository;
            _rateLimitPolicyRepository = rateLimitPolicyRepository;
            _allowedIpRepository = allowedIpRepository;


            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateClientCredentialAsync(ClientCredentialCreate input)
        {
            // 1. ClientCredentialEntity oluştur
            var oClientCredential = ClientCredentialEntity.Create(AesEncryption.Encrypt(Guid.NewGuid().ToString()), input.RequestLimit, input.ExpirationDate);
            await _clientCredentialRepository.AddAsync(oClientCredential);


            // 2. RateLimitPolicyEntity oluştur
            var oClientRateLimitPolicy = ClientRateLimitPolicyEntity.Create(input.MaxRequestsPerMinute, input.MaxRequestsPerHour, input.MaxRequestsPerDay, oClientCredential);
            await _rateLimitPolicyRepository.AddAsync(oClientRateLimitPolicy);

            // 3. IP listesi varsa AllowedIpEntity kayıtları oluştur
            if (input.IpList?.Any() == true)
            {
                foreach (var ip in input.IpList)
                {
                    var allowIp = ClientAllowedIpEntity.Create(ip, oClientRateLimitPolicy);
                    await _allowedIpRepository.AddAsync(allowIp);
                }
            }

            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
