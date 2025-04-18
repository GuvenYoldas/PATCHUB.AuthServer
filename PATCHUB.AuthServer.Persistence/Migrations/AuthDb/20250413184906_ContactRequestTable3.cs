using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATCHUB.AuthServer.Persistence.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class ContactRequestTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "dbo",
                table: "CONTACT_REQUEST",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "dbo",
                table: "CONTACT_REQUEST");
        }
    }
}
