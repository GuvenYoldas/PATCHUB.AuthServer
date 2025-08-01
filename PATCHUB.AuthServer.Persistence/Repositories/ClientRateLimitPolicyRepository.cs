using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class ClientRateLimitPolicyRepository : GenericRepository<ClientRateLimitPolicyEntity, int>, IClientRateLimitPolicyRepository
    {
        public ClientRateLimitPolicyRepository(AuthDbContext context, IClientCredentialAccessor accessor) : base(context, accessor)
        {

        }

    }
}
