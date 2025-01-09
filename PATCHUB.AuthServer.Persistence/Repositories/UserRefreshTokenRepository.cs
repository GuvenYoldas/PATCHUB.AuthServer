using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Entities.Base;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshTokenEntity>
    {
        protected readonly AuthDbContext _context;
        public UserRefreshTokenRepository(AuthDbContext context) : base(context)
        {
            _context = context;
        }

        public void Insert(UserRefreshTokenEntity entity)
        {
            var sql = "INSERT INTO dbo.USER_REFRESH_TOKEN (IDUser, Token, ExpirationDate) VALUES (@IDUser, @Token, @ExpirationDate)";

            _context.Database.ExecuteSqlRaw(sql,
                new SqlParameter("@IDUser", entity.IDUser),
                new SqlParameter("@Token", entity.Token),
                new SqlParameter("@ExpirationDate", entity.ExpirationDate));
        }
    }
}
