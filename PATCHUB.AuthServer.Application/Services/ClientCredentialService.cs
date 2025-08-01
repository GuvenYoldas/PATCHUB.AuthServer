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

        //private readonly IClientRateLimitPolicyRepository _rateLimitPolicyRepository;
        //private readonly IClientAllowedIpRepository _allowedIpRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ClientCredentialService(
            IClientCredentialRepository clientCredentialRepository,
            //IClientRateLimitPolicyRepository rateLimitPolicyRepository,
            //IClientAllowedIpRepository allowedIpRepository
            IUnitOfWork unitOfWork
            )
        {
            _clientCredentialRepository = clientCredentialRepository;
            _unitOfWork = unitOfWork;
            //_rateLimitPolicyRepository = rateLimitPolicyRepository;
            //_allowedIpRepository = allowedIpRepository;
        }

        public async Task<bool> CreateClientCredentialAsync(ClientCredentialCreate input)
        {
            // 1. ClientCredentialEntity oluştur
            var clientCredential = new ClientCredentialEntity
            {
                IDClient = Guid.NewGuid(),
                SecretHash = AesEncryption.Encrypt(Guid.NewGuid().ToString()),
                RequestLimit = input.RequestLimit,
                RequestCount = 0,
                ExpirationDate = input.ExpirationDate,
                StatusCode = (int)StatusCode.ACTIVE
            };

             _clientCredentialRepository.Created(clientCredential);
          

            return true;
            //// 2. RateLimitPolicyEntity oluştur
            //var rateLimitPolicy = new ClientRateLimitPolicyEntity
            //{
            //    IDClientCredential = clientCredential.ID,
            //    MaxRequestsPerMinute = input.MaxRequestsPerMinute,
            //    MaxRequestsPerHour = input.MaxRequestsPerHour,
            //    MaxRequestsPerDay = input.MaxRequestsPerDay,
            //    StatusCode = (int)StatusCode.ACTIVE
            //};

            //await _rateLimitPolicyRepository.CreateAsync(rateLimitPolicy);

            //// 3. IP listesi varsa AllowedIpEntity kayıtları oluştur
            //if (input.IpList != null && input.IpList.Any())
            //{
            //    var allowedIps = input.IpList.Select(ip => new ClientAllowedIpEntity
            //    {
            //        IDRateLimitPolicy = rateLimitPolicy.ID,
            //        IpAddress = ip,
            //        StatusCode = (int)StatusCode.ACTIVE
            //    }).ToList();

            //    await _allowedIpRepository.CreateRangeAsync(allowedIps);
            //}

            await _unitOfWork.SaveAsync();

           // return clientCredential;
        }
    }
}
