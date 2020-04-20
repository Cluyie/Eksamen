using Microsoft.EntityFrameworkCore.Migrations;

namespace UCLDreamTeam.User.Data.Migrations
{
    public partial class NormalizedUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Users");
        }
    }
}
