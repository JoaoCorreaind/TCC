using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class tagroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Grupo_GrupoId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_GrupoId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "GrupoId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "GrupoTag",
                columns: table => new
                {
                    GruposId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoTag", x => new { x.GruposId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GrupoTag_Grupo_GruposId",
                        column: x => x.GruposId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoTag_TagsId",
                table: "GrupoTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoTag");

            migrationBuilder.AddColumn<int>(
                name: "GrupoId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_GrupoId",
                table: "Tag",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Grupo_GrupoId",
                table: "Tag",
                column: "GrupoId",
                principalTable: "Grupo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
