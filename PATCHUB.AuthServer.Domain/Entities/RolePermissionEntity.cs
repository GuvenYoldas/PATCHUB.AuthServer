
namespace PATCHUB.AuthServer.Domain.Entities
{
    public class RolePermissionEntity
    {
        public int IDRole { get; set; }
        public int IDPermission { get; set; }

        public RoleEntity Role { get; set; } = null!;
        public PermissionEntity Permission { get; set; } = null!;
    }
}
