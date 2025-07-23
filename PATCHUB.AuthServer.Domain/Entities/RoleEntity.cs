using PATCHUB.AuthServer.Domain.Entities.Base;


namespace PATCHUB.AuthServer.Domain.Entities
{
    public class RoleEntity : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
