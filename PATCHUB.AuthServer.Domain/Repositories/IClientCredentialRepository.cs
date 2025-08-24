using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories.Base;

namespace PATCHUB.AuthServer.Domain.Repositories
{
    public interface IClientCredentialRepository : IAuditableRepositoryBase<ClientCredentialEntity, int>
    {

    }
}