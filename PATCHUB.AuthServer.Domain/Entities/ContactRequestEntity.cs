using PATCHUB.AuthServer.Domain.Common.Primitives;
using PATCHUB.AuthServer.Domain.Enumeration;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ContactRequestEntity : BaseEntity
    {
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        private ContactRequestEntity() { } // EF için

        public static ContactRequestEntity Create(string fullName, string mail, string messageText, string location) // constructure
        {
            return new ContactRequestEntity
            {
                FullName = fullName,
                Mail = mail,
                MessageText = messageText,
                CreatedDate = DateTime.UtcNow,
                Location = location
            };
        }
    }
}
