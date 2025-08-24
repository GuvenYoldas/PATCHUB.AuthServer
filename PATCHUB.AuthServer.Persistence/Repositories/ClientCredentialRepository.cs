using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class ClientCredentialRepository : AuditableRepositoryBase<ClientCredentialEntity, int>, IClientCredentialRepository
    {
        public ClientCredentialRepository(AuthDbContext context, IClientCredentialAccessor accessor) : base(context, accessor)
        {

        }

    }

}