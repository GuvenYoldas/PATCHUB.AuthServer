using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATCHUB.AuthServer.Persistence.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class baseauthupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_ALLOWED_IP_CLIENT_RATE_LIMIT_POLICY_IDPolicy",
                table: "CLIENT_ALLOWED_IP");

            migrationBuilder.RenameColumn(
                name: "IDPolicy",
                table: "CLIENT_ALLOWED_IP",
                newName: "IDRateLimitPolicy");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENT_ALLOWED_IP_IDPolicy",
                table: "CLIENT_ALLOWED_IP",
                newName: "IX_CLIENT_ALLOWED_IP_IDRateLimitPolicy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "USER_ROLE",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "ROLE",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "PERMISSION",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "CLIENT_RATE_LIMIT_POLICY",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "CLIENT_CREDENTIAL",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "CLIENT_ALLOWED_IP",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "API_USAGE_LOG",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENT_ALLOWED_IP_CLIENT_RATE_LIMIT_POLICY_IDRateLimitPolicy",
                table: "CLIENT_ALLOWED_IP",
                column: "IDRateLimitPolicy",
                principalTable: "CLIENT_RATE_LIMIT_POLICY",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_ALLOWED_IP_CLIENT_RATE_LIMIT_POLICY_IDRateLimitPolicy",
                table: "CLIENT_ALLOWED_IP");

            migrationBuilder.RenameColumn(
                name: "IDRateLimitPolicy",
                table: "CLIENT_ALLOWED_IP",
                newName: "IDPolicy");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENT_ALLOWED_IP_IDRateLimitPolicy",
                table: "CLIENT_ALLOWED_IP",
                newName: "IX_CLIENT_ALLOWED_IP_IDPolicy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "USER_ROLE",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "ROLE",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "PERMISSION",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "CLIENT_RATE_LIMIT_POLICY",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "CLIENT_CREDENTIAL",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "CLIENT_ALLOWED_IP",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATE_DATE",
                table: "API_USAGE_LOG",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENT_ALLOWED_IP_CLIENT_RATE_LIMIT_POLICY_IDPolicy",
                table: "CLIENT_ALLOWED_IP",
                column: "IDPolicy",
                principalTable: "CLIENT_RATE_LIMIT_POLICY",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
