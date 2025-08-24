using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories.Base;

namespace PATCHUB.AuthServer.Domain.Repositories
{
    public interface IContactRequestRepository 
    {
        bool Insert(ContactRequestEntity entity);
    }
}