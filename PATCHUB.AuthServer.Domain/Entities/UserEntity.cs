using PATCHUB.AuthServer.Domain.Common.Primitives;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class UserEntity: AuditableEntity<int>
    {
        public UserEntity()
        {
        }
        public string? IdentityNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? UserName { get; set; }
        public string? CountryCode { get; set; } = "+90";
        public string? PhoneNo { get; set; }
        public string Mail { get; set; }
        public string? AddressBill { get; set; }
        public string? AddressShipping { get; set; }
        public string? AvatarUrl { get; set; }
        public string SaltString { get; set; }
        public string PasswordHash { get; set; }
        public decimal Balance { get; set; } = 0;
        public int UserType { get; set; } = 100;
        public string? ActivatorKey { get; set; }
        public string? ReferenceUser { get; set; }


    }
}
