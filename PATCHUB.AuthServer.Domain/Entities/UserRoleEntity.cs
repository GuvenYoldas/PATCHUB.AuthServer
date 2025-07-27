using PATCHUB.AuthServer.Domain.Entities.Base;


namespace PATCHUB.AuthServer.Domain.Entities
{

    public class UserRoleEntity : BaseEntity<int>
    {
        public int IDUser { get; set; }
        public int IDRole { get; set; }
        public RoleEntity Role { get; set; }
    }
}
