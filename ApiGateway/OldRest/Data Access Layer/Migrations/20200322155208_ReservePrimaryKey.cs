using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class ReservePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_ReserveTime",
                "ReserveTime");

            migrationBuilder.DropIndex(
                "IX_ReserveTime_ReservationId",
                "ReserveTime");

            migrationBuilder.DropColumn(
                "Id",
                "ReserveTime");

            migrationBuilder.AddPrimaryKey(
                "PK_ReserveTime",
                "ReserveTime",
                "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_ReserveTime",
                "ReserveTime");

            migrationBuilder.AddColumn<Guid>(
                "Id",
                "ReserveTime",
                "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                "PK_ReserveTime",
                "ReserveTime",
                "Id");

            migrationBuilder.CreateIndex(
                "IX_ReserveTime_ReservationId",
                "ReserveTime",
                "ReservationId",
                unique: true);
        }
    }
}