using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PATCHUB.AuthServer.Application.Dtos;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Entities.Base;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Helpers;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity, int>
    {
        protected readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(AppUserCreate input)
        {
            string saltStr = "";
            string passHass = Argon2.HashPassword(input.Password, out saltStr);

            var oUserEntity = new UserEntity
            {
                ActivatorKey = Guid.NewGuid().ToString(),
                LastName = input.LastName,
                Mail = input.Mail,
                Name = input.Name,
                PasswordHash = passHass,
                ReferenceUser = input.ReferenceUser?.Trim(),
                SaltString = saltStr,
                StatusCode = 200
            };


            await _context.User.AddAsync(oUserEntity);
            int result = await _context.SaveChangesAsync();

            return result > 0;

        }
    }
}
