using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Entities.Base;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity, int> 
    {
        protected readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
