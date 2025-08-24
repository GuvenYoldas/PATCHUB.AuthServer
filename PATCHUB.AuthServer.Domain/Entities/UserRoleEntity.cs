using PATCHUB.AuthServer.Domain.Common.Primitives;

namespace PATCHUB.AuthServer.Domain.Entities
{

    public class UserRoleEntity : AuditableEntity<int>
    {
        public int IDUser { get; set; }
        public int IDRole { get; set; }
        public RoleEntity Role { get; set; }
    }
}
