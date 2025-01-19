using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATCHUB.AuthServer.Persistence.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "USER",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressBill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressShipping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaltString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    UserType = table.Column<int>(type: "int", nullable: false, defaultValue: 100),
                    ActivatorKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATUS_CODE = table.Column<int>(type: "int", nullable: false),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_USERID = table.Column<int>(type: "int", nullable: true),
                    UPDATE_IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATE_USERID = table.Column<int>(type: "int", nullable: true),
                    CREATE_IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_IdentityNumber",
                schema: "dbo",
                table: "USER",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_Mail",
                schema: "dbo",
                table: "USER",
                column: "Mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_PhoneNo",
                schema: "dbo",
                table: "USER",
                column: "PhoneNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER",
                schema: "dbo");
        }
    }
}
