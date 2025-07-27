using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATCHUB.AuthServer.Persistence.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class authclientandlogtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IDClientCredential",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "API_USAGE_LOG",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDClientCredential = table.Column<int>(type: "int", nullable: false),
                    IDUser = table.Column<int>(type: "int", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ExtraDataJson = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_USERID = table.Column<int>(type: "int", nullable: true),
                    UPDATE_IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATE_USERID = table.Column<int>(type: "int", nullable: true),
                    CREATE_IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_USAGE_LOG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CLIENT_CREDENTIAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecretHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    RequestLimit = table.Column<int>(type: "int", nullable: false),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_CLIENT_CREDENTIAL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LOGIN_HISTORY",
                columns: table => new
                {
                    IDClientCredential = table.Column<int>(type: "int", nullable: false),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PERMISSION",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_PERMISSION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TOKEN_BLACKLIST",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CLIENT_RATE_LIMIT_POLICY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDClientCredential = table.Column<int>(type: "int", nullable: false),
                    MaxRequestsPerMinute = table.Column<int>(type: "int", nullable: false),
                    MaxRequestsPerHour = table.Column<int>(type: "int", nullable: false),
                    MaxRequestsPerDay = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_CLIENT_RATE_LIMIT_POLICY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CLIENT_RATE_LIMIT_POLICY_CLIENT_CREDENTIAL_IDClientCredential",
                        column: x => x.IDClientCredential,
                        principalTable: "CLIENT_CREDENTIAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IDClientCredential = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ROLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ROLE_CLIENT_CREDENTIAL_IDClientCredential",
                        column: x => x.IDClientCredential,
                        principalTable: "CLIENT_CREDENTIAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLIENT_ALLOWED_IP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPolicy = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
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
                    table.PrimaryKey("PK_CLIENT_ALLOWED_IP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CLIENT_ALLOWED_IP_CLIENT_RATE_LIMIT_POLICY_IDPolicy",
                        column: x => x.IDPolicy,
                        principalTable: "CLIENT_RATE_LIMIT_POLICY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_PERMISSION",
                columns: table => new
                {
                    IDRole = table.Column<int>(type: "int", nullable: false),
                    IDPermission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_PERMISSION", x => new { x.IDRole, x.IDPermission });
                    table.ForeignKey(
                        name: "FK_ROLE_PERMISSION_PERMISSION_IDPermission",
                        column: x => x.IDPermission,
                        principalTable: "PERMISSION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ROLE_PERMISSION_ROLE_IDRole",
                        column: x => x.IDRole,
                        principalTable: "ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDRole = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_USER_ROLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_ROLE_IDRole",
                        column: x => x.IDRole,
                        principalTable: "ROLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_REFRESH_TOKEN_IDClientCredential",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                column: "IDClientCredential");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_ALLOWED_IP_IDPolicy",
                table: "CLIENT_ALLOWED_IP",
                column: "IDPolicy");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_CREDENTIAL_IDClient_SecretHash",
                table: "CLIENT_CREDENTIAL",
                columns: new[] { "IDClient", "SecretHash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_RATE_LIMIT_POLICY_IDClientCredential",
                table: "CLIENT_RATE_LIMIT_POLICY",
                column: "IDClientCredential");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_IDClientCredential_Name",
                table: "ROLE",
                columns: new[] { "IDClientCredential", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_PERMISSION_IDPermission",
                table: "ROLE_PERMISSION",
                column: "IDPermission");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_IDRole",
                table: "USER_ROLE",
                column: "IDRole");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_REFRESH_TOKEN_CLIENT_CREDENTIAL_IDClientCredential",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                column: "IDClientCredential",
                principalTable: "CLIENT_CREDENTIAL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_REFRESH_TOKEN_CLIENT_CREDENTIAL_IDClientCredential",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN");

            migrationBuilder.DropTable(
                name: "API_USAGE_LOG");

            migrationBuilder.DropTable(
                name: "CLIENT_ALLOWED_IP");

            migrationBuilder.DropTable(
                name: "LOGIN_HISTORY");

            migrationBuilder.DropTable(
                name: "ROLE_PERMISSION");

            migrationBuilder.DropTable(
                name: "TOKEN_BLACKLIST");

            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "CLIENT_RATE_LIMIT_POLICY");

            migrationBuilder.DropTable(
                name: "PERMISSION");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "CLIENT_CREDENTIAL");

            migrationBuilder.DropIndex(
                name: "IX_USER_REFRESH_TOKEN_IDClientCredential",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "IDClientCredential",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "dbo",
                table: "USER_REFRESH_TOKEN",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);
        }
    }
}
