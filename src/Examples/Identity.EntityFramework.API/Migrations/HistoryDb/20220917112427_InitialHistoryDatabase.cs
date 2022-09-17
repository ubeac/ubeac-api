using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.EntityFramework.API.Migrations.HistoryDb
{
    public partial class InitialHistoryDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Data_UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    Data_PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    Data_TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: true),
                    Data_LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Data_LockoutEnabled = table.Column<bool>(type: "bit", nullable: true),
                    Data_AccessFailedCount = table.Column<int>(type: "int", nullable: true),
                    Data_LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data_LoginsCount = table.Column<int>(type: "int", nullable: true),
                    Data_LastPasswordChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data_LastPasswordChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Enabled = table.Column<bool>(type: "bit", nullable: true),
                    Data_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data_LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data_AuthenticatorKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
