using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using PATCHUB.AuthServer.Domain.Repositories;

namespace PATCHUB.AuthServer.Application.Services
{
    public class ClientRateLimitPolicyService : IClientRateLimitPolicyService
    {
        private readonly IClientRateLimitPolicyRepository _clientRateLimitPolicyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientRateLimitPolicyService(
            IClientRateLimitPolicyRepository clientRateLimitPolicyRepository,
            IUnitOfWork unitOfWork
            )
        {
            _clientRateLimitPolicyRepository = clientRateLimitPolicyRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
