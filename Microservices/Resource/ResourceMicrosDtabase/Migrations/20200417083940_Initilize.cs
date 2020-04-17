using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceMicrosDtabase.Migrations
{
    public partial class Initilize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvaiableTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    Recurring = table.Column<bool>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    ResourceAvaiableTimeId = table.Column<Guid>(name: "Resource<AvaiableTime>Id", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaiableTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvaiableTimes_Resources_Resource<AvaiableTime>Id",
                        column: x => x.ResourceAvaiableTimeId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaiableTimes_Resource<AvaiableTime>Id",
                table: "AvaiableTimes",
                column: "Resource<AvaiableTime>Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaiableTimes");

            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
