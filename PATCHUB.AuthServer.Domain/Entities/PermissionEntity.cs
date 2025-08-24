using PATCHUB.AuthServer.Domain.Common.Primitives;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class PermissionEntity : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
