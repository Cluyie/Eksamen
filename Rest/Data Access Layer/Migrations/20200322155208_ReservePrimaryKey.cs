using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class ReservePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveTime",
                table: "ReserveTime");

            migrationBuilder.DropIndex(
                name: "IX_ReserveTime_ReservationId",
                table: "ReserveTime");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReserveTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveTime",
                table: "ReserveTime",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReserveTime",
                table: "ReserveTime");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ReserveTime",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReserveTime",
                table: "ReserveTime",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveTime_ReservationId",
                table: "ReserveTime",
                column: "ReservationId",
                unique: true);
        }
    }
}
