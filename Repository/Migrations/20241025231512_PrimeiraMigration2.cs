using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.CreateIndex(
                name: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA",
                column: "CODIGOUSUARIO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.CreateIndex(
                name: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA",
                column: "CODIGOUSUARIO");
        }
    }
}
