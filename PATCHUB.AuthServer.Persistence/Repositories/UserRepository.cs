using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PATCHUB.AuthServer.Application.Dtos;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Enumeration;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;
using PATCHUB.SharedLibrary.Helpers;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class UserRepository : AuditableRepositoryBase<UserEntity, int>
    {
        protected readonly AppDbContext _context;
        public UserRepository(AppDbContext context, IClientCredentialAccessor accessor) : base(context, accessor)
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
                Status= EnumStatusCode.WAITING_APPROVE
            };


            await _context.User.AddAsync(oUserEntity);
            int result = await _context.SaveChangesAsync();

            return result > 0;

        }
    }
}
