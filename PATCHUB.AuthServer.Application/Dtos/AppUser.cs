using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATCHUB.AuthServer.Application.Dtos
{
    public class AppUser 
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNo { get; set; }
        public string Mail { get; set; }
        public string AddressBill { get; set; }
        public string AddressShipping { get; set; }
        public string AvatarUrl { get; set; }
        public decimal Balance { get; set; }
        public int UserType { get; set; }
        public string ReferenceUser { get; set; }
    }
}
