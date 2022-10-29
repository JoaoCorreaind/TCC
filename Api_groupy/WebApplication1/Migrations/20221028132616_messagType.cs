using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class messagType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendShipId",
                table: "ChatMessage",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ChatMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_FriendShipId",
                table: "ChatMessage",
                column: "FriendShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_FriendShip_FriendShipId",
                table: "ChatMessage",
                column: "FriendShipId",
                principalTable: "FriendShip",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_FriendShip_FriendShipId",
                table: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_FriendShipId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "FriendShipId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ChatMessage");
        }
    }
}
