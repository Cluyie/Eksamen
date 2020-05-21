using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class InitialApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Resources",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Resources", x => x.Id); });

            migrationBuilder.CreateTable(
                "AvailableTime",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Available = table.Column<bool>(),
                    Recurring = table.Column<int>(nullable: true),
                    From = table.Column<DateTime>(),
                    To = table.Column<DateTime>(),
                    ResourceId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableTime", x => x.Id);
                    table.ForeignKey(
                        "FK_AvailableTime_Resources_ResourceId",
                        x => x.ResourceId,
                        "Resources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Reservations",
                table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    ResourceId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        "FK_Reservations_Resources_ResourceId",
                        x => x.ResourceId,
                        "Resources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ReserveTime",
                table => new
                {
                    Id = table.Column<Guid>(),
                    FromDate = table.Column<DateTime>(),
                    ToDate = table.Column<DateTime>(),
                    ReservationId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveTime", x => x.Id);
                    table.ForeignKey(
                        "FK_ReserveTime_Reservations_ReservationId",
                        x => x.ReservationId,
                        "Reservations",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_AvailableTime_ResourceId",
                "AvailableTime",
                "ResourceId");

            migrationBuilder.CreateIndex(
                "IX_Reservations_ResourceId",
                "Reservations",
                "ResourceId");

            migrationBuilder.CreateIndex(
                "IX_ReserveTime_ReservationId",
                "ReserveTime",
                "ReservationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AvailableTime");

            migrationBuilder.DropTable(
                "ReserveTime");

            migrationBuilder.DropTable(
                "Reservations");

            migrationBuilder.DropTable(
                "Resources");
        }
    }
}