using PATCHUB.AuthServer.Application.Services.Interfaces;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using PATCHUB.AuthServer.Domain.Repositories;

namespace PATCHUB.AuthServer.Application.Services
{
    public class ClientAllowedIpService : IClientAllowedIpService
    {
        private readonly IClientAllowedIpRepository _clientAllowedIpRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientAllowedIpService(
            IClientAllowedIpRepository clientAllowedIpRepository,
            IUnitOfWork unitOfWork
            )
        {
            _clientAllowedIpRepository = clientAllowedIpRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
