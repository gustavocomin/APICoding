using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATUALIZACAOTAREFA_TAREFA_IDTAREFA",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.DropForeignKey(
                name: "FK_ATUALIZACAOTAREFA_USUARIO_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.DropForeignKey(
                name: "FK_COMENTARIO_TAREFA_IDTAREFA",
                table: "COMENTARIO");

            migrationBuilder.DropForeignKey(
                name: "FK_TAREFA_PROJETO_IDPROJETO",
                table: "TAREFA");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                table: "TAREFA",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "PRIORIDADE",
                table: "TAREFA",
                newName: "Prioridade");

            migrationBuilder.RenameColumn(
                name: "IDPROJETO",
                table: "TAREFA",
                newName: "IdProjeto");

            migrationBuilder.RenameColumn(
                name: "IDTAREFA",
                table: "TAREFA",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TAREFA_IDPROJETO",
                table: "TAREFA",
                newName: "IX_TAREFA_IdProjeto");

            migrationBuilder.RenameColumn(
                name: "DATACRIACAO",
                table: "PROJETO",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "IDPROJETO",
                table: "PROJETO",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TEXTO",
                table: "COMENTARIO",
                newName: "Texto");

            migrationBuilder.RenameColumn(
                name: "IDTAREFA",
                table: "COMENTARIO",
                newName: "IdTarefa");

            migrationBuilder.RenameColumn(
                name: "CODIGOCOMENTARIO",
                table: "COMENTARIO",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_COMENTARIO_IDTAREFA",
                table: "COMENTARIO",
                newName: "IX_COMENTARIO_IdTarefa");

            migrationBuilder.RenameColumn(
                name: "IDTAREFA",
                table: "ATUALIZACAOTAREFA",
                newName: "IdTarefa");

            migrationBuilder.RenameColumn(
                name: "DATAALTERACAO",
                table: "ATUALIZACAOTAREFA",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA",
                newName: "CodigoUsuario");

            migrationBuilder.RenameColumn(
                name: "OBSERVACAO",
                table: "ATUALIZACAOTAREFA",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "IDATUALIZACAOTAREFAS",
                table: "ATUALIZACAOTAREFA",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ATUALIZACAOTAREFA_IDTAREFA",
                table: "ATUALIZACAOTAREFA",
                newName: "IX_ATUALIZACAOTAREFA_IdTarefa");

            migrationBuilder.RenameIndex(
                name: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA",
                newName: "IX_ATUALIZACAOTAREFA_CodigoUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ATUALIZACAOTAREFA_TAREFA_IdTarefa",
                table: "ATUALIZACAOTAREFA",
                column: "IdTarefa",
                principalTable: "TAREFA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ATUALIZACAOTAREFA_USUARIO_CodigoUsuario",
                table: "ATUALIZACAOTAREFA",
                column: "CodigoUsuario",
                principalTable: "USUARIO",
                principalColumn: "IDUSUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COMENTARIO_TAREFA_IdTarefa",
                table: "COMENTARIO",
                column: "IdTarefa",
                principalTable: "TAREFA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAREFA_PROJETO_IdProjeto",
                table: "TAREFA",
                column: "IdProjeto",
                principalTable: "PROJETO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATUALIZACAOTAREFA_TAREFA_IdTarefa",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.DropForeignKey(
                name: "FK_ATUALIZACAOTAREFA_USUARIO_CodigoUsuario",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.DropForeignKey(
                name: "FK_COMENTARIO_TAREFA_IdTarefa",
                table: "COMENTARIO");

            migrationBuilder.DropForeignKey(
                name: "FK_TAREFA_PROJETO_IdProjeto",
                table: "TAREFA");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TAREFA",
                newName: "STATUS");

            migrationBuilder.RenameColumn(
                name: "Prioridade",
                table: "TAREFA",
                newName: "PRIORIDADE");

            migrationBuilder.RenameColumn(
                name: "IdProjeto",
                table: "TAREFA",
                newName: "IDPROJETO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TAREFA",
                newName: "IDTAREFA");

            migrationBuilder.RenameIndex(
                name: "IX_TAREFA_IdProjeto",
                table: "TAREFA",
                newName: "IX_TAREFA_IDPROJETO");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "PROJETO",
                newName: "DATACRIACAO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PROJETO",
                newName: "IDPROJETO");

            migrationBuilder.RenameColumn(
                name: "Texto",
                table: "COMENTARIO",
                newName: "TEXTO");

            migrationBuilder.RenameColumn(
                name: "IdTarefa",
                table: "COMENTARIO",
                newName: "IDTAREFA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "COMENTARIO",
                newName: "CODIGOCOMENTARIO");

            migrationBuilder.RenameIndex(
                name: "IX_COMENTARIO_IdTarefa",
                table: "COMENTARIO",
                newName: "IX_COMENTARIO_IDTAREFA");

            migrationBuilder.RenameColumn(
                name: "IdTarefa",
                table: "ATUALIZACAOTAREFA",
                newName: "IDTAREFA");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "ATUALIZACAOTAREFA",
                newName: "DATAALTERACAO");

            migrationBuilder.RenameColumn(
                name: "CodigoUsuario",
                table: "ATUALIZACAOTAREFA",
                newName: "CODIGOUSUARIO");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ATUALIZACAOTAREFA",
                newName: "OBSERVACAO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ATUALIZACAOTAREFA",
                newName: "IDATUALIZACAOTAREFAS");

            migrationBuilder.RenameIndex(
                name: "IX_ATUALIZACAOTAREFA_IdTarefa",
                table: "ATUALIZACAOTAREFA",
                newName: "IX_ATUALIZACAOTAREFA_IDTAREFA");

            migrationBuilder.RenameIndex(
                name: "IX_ATUALIZACAOTAREFA_CodigoUsuario",
                table: "ATUALIZACAOTAREFA",
                newName: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_ATUALIZACAOTAREFA_TAREFA_IDTAREFA",
                table: "ATUALIZACAOTAREFA",
                column: "IDTAREFA",
                principalTable: "TAREFA",
                principalColumn: "IDTAREFA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ATUALIZACAOTAREFA_USUARIO_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA",
                column: "CODIGOUSUARIO",
                principalTable: "USUARIO",
                principalColumn: "IDUSUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COMENTARIO_TAREFA_IDTAREFA",
                table: "COMENTARIO",
                column: "IDTAREFA",
                principalTable: "TAREFA",
                principalColumn: "IDTAREFA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TAREFA_PROJETO_IDPROJETO",
                table: "TAREFA",
                column: "IDPROJETO",
                principalTable: "PROJETO",
                principalColumn: "IDPROJETO",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
