using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories.Base;

namespace PATCHUB.AuthServer.Domain.Repositories
{
    public interface IClientAllowedIpRepository : IAuditableRepositoryBase<ClientAllowedIpEntity, int>
    {

    }
}
