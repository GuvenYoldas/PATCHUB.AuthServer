using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Repositories
{
    public interface IClientCredentialRepository : IGenericRepository<ClientCredentialEntity, int>
    {

    }
}