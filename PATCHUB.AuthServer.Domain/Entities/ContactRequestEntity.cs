using PATCHUB.AuthServer.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Domain.Entities
{
    public class ContactRequestEntity : BaseEntity
    {
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Location { get; set; }
    }
}
