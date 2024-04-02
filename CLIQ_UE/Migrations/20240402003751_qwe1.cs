using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLIQ_UE.Migrations
{
    /// <inheritdoc />
    public partial class qwe1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OnlineUsers");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "OnlineUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ChatIndividual",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReaded",
                table: "ChatIndividual",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "OnlineUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ChatIndividual");

            migrationBuilder.DropColumn(
                name: "IsReaded",
                table: "ChatIndividual");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OnlineUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
