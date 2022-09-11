using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.EntityFramework.API.Migrations
{
    public partial class InitialHttpLoggingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HttpLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_DisplayUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Protocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Scheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_PathBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_QueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_ContentLength = table.Column<long>(type: "bigint", nullable: true),
                    Request_Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response_ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response_ContentLength = table.Column<long>(type: "bigint", nullable: true),
                    Response_Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response_Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    MemoryUsage = table.Column<long>(type: "bigint", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvironmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvironmentUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception_HelpLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception_HResult = table.Column<int>(type: "int", nullable: true),
                    Exception_Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception_Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception_StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HttpLog");
        }
    }
}
