using PATCHUB.AuthServer.Domain.Entities.Base;


namespace PATCHUB.AuthServer.Domain.Entities
{
    public class RoleEntity : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public Guid IDClient { get; set; }
        public ClientCredentialEntity Client { get; set; } = null!;

        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    }
}
