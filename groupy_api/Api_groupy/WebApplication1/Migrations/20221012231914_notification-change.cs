using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class notificationchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notification",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                table: "Notification",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResolvedResult",
                table: "Notification",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_SenderUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_SenderUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsResolved",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ResolvedResult",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "SenderUserId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ReciverUserId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SenderUserId1",
                table: "Notification",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SenderUserId1",
                table: "Notification",
                column: "SenderUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_SenderUserId1",
                table: "Notification",
                column: "SenderUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
