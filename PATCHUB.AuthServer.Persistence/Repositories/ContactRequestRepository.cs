using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PATCHUB.AuthServer.Domain.Entities;
using PATCHUB.AuthServer.Domain.Repositories;
using PATCHUB.AuthServer.Persistence.Context;
using PATCHUB.AuthServer.Persistence.Repositories.Base;
using PATCHUB.SharedLibrary.Abstractions;

namespace PATCHUB.AuthServer.Persistence.Repositories
{
    public class ContactRequestRepository : IContactRequestRepository
    {
        protected readonly AuthDbContext _context;
        public ContactRequestRepository(AuthDbContext context)
        {
            _context = context;
        }


        public bool Insert(ContactRequestEntity entity)
        {
            try
            {
                var sql = "INSERT INTO dbo.CONTACT_REQUEST (FullName, Mail, MessageText, CreatedDate, Location) VALUES (@FullName, @Mail, @MessageText, @CreatedDate, @Location)";

                var affectedRows = _context.Database.ExecuteSqlRaw(sql,
                    new SqlParameter("@FullName", entity.FullName),
                    new SqlParameter("@Mail", entity.Mail),
                    new SqlParameter("@CreatedDate", DateTime.UtcNow),
                    new SqlParameter("@MessageText", entity.MessageText),
                    new SqlParameter("@Location", entity.Location));

                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
