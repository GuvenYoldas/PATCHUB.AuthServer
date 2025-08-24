using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class ClientAllowedIpRepository : AuditableRepositoryBase<ClientAllowedIpEntity, int>, IClientAllowedIpRepository
    {
        public ClientAllowedIpRepository(AuthDbContext context, IClientCredentialAccessor accessor) : base(context, accessor)
        {

        }

    }

}
