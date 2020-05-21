using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UCLDreamTeam.Mail.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("fdeb2144-e949-4db9-9019-cd5918cb248e"), "Hegnet's Sure Sokker" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("efe2f55b-a038-42e3-98ff-57a5e906880a"), "DreamTeamUCL@gmail.com", "Admin", "Adminsen" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
