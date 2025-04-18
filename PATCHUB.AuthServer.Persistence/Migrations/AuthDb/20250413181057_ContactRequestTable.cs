using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATCHUB.AuthServer.Persistence.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class ContactRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONTACT_REQUEST",
                schema: "dbo",
                columns: table => new
                {
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTACT_REQUEST",
                schema: "dbo");
        }
    }
}
