using PATCHUB.AuthServer.Application.Dtos.ClientCredential;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Enumeration;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;
using PATCHUB.SharedLibrary.Helpers;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class ClientCredentialRepository : GenericRepository<ClientCredentialEntity, int>, IClientCredentialRepository
    {
        public ClientCredentialRepository(AuthDbContext context, IClientCredentialAccessor accessor) : base(context, accessor)
        {

        }

    }

}