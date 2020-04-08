using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UCLDreamTeam.Reservation.Data.Migrations
{
    public partial class Initalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Reservations",
                table => new
                {
                    Id = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    ResourceId = table.Column<Guid>()
                },
                constraints: table => { table.PrimaryKey("PK_Reservations", x => x.Id); });

            migrationBuilder.CreateTable(
                "ReserveTime",
                table => new
                {
                    ReservationId = table.Column<Guid>(),
                    FromDate = table.Column<DateTime>(),
                    ToDate = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveTime", x => x.ReservationId);
                    table.ForeignKey(
                        "FK_ReserveTime_Reservations_ReservationId",
                        x => x.ReservationId,
                        "Reservations",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ReserveTime");

            migrationBuilder.DropTable(
                "Reservations");
        }
    }
}