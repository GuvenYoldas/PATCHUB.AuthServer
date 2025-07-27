using PATCHUB.AuthServer.Domain.Entities.Base;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class LoginHistoryEntity : BaseEntity
    {
        public int IDClientCredential { get; set; }
        public int IDUser { get; set; }
        public DateTime LoginDate { get; set; } = DateTime.UtcNow;
        public string IP { get; set; }
        public string UserAgent { get; set; }
    }
}
