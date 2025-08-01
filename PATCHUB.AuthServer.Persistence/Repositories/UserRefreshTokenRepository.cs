using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Entities.Base;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.ErrorHandling.Exceptions;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshTokenEntity>
    {
        protected readonly AuthDbContext _context;
        public UserRefreshTokenRepository(AuthDbContext context) : base(context)
        {
            _context = context;
        }

        public bool Insert(UserRefreshTokenEntity entity)
        {
            try
            {
                var sql = "INSERT INTO dbo.USER_REFRESH_TOKEN (IDUser, Token, ExpirationDate) VALUES (@IDUser, @Token, @ExpirationDate)";

                _context.Database.ExecuteSqlRaw(sql,
                    new SqlParameter("@IDUser", entity.IDUser),
                    new SqlParameter("@Token", entity.Token),
                    new SqlParameter("@ExpirationDate", entity.ExpirationDate),
                    new SqlParameter("@IDClientCredential", entity.IDClientCredential)
                    );
            }
            catch (Exception ex)
            {
                throw new ServiceUnavailableException("Refresh Token kaydedilemedi! " + ex.Message);
            }
            return true;
        }

        public bool Update(UserRefreshTokenEntity entity)
        {
            var sql = "UPDATE dbo.USER_REFRESH_TOKEN " +
                      "SET Token = @Token, ExpirationDate = @ExpirationDate " +
                      "WHERE IDUser = @IDUser";

            var affectedRows = _context.Database.ExecuteSqlRaw(sql,
                new SqlParameter("@IDUser", entity.IDUser),
                new SqlParameter("@Token", entity.Token),
                new SqlParameter("@ExpirationDate", entity.ExpirationDate));

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                throw new ServiceUnavailableException("Güncellenecek Refresh token bulunamadı!");
            }

        }

        public bool Delete(int userId)
        {

            var sql = "DELETE FROM dbo.USER_REFRESH_TOKEN WHERE IDUser = @IDUser";

            var affectedRows = _context.Database.ExecuteSqlRaw(sql,
                new SqlParameter("@IDUser", userId));

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                throw new ServiceUnavailableException("Silinecek token bulunamadı!");
            }

        }
    }
}
