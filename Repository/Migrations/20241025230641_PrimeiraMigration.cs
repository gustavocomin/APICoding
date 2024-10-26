using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROJETO",
                columns: table => new
                {
                    IDPROJETO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJETO", x => x.IDPROJETO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    IDUSUARIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NOME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LOGIN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ACESSO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.IDUSUARIO);
                });

            migrationBuilder.CreateTable(
                name: "TAREFA",
                columns: table => new
                {
                    IDTAREFA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PRIORIDADE = table.Column<int>(type: "integer", nullable: false),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    IDPROJETO = table.Column<int>(type: "integer", nullable: false),
                    ProjetoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREFA", x => x.IDTAREFA);
                    table.ForeignKey(
                        name: "FK_TAREFA_PROJETO_IDPROJETO",
                        column: x => x.IDPROJETO,
                        principalTable: "PROJETO",
                        principalColumn: "IDPROJETO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TAREFA_PROJETO_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "PROJETO",
                        principalColumn: "IDPROJETO");
                });

            migrationBuilder.CreateTable(
                name: "ATUALIZACAOTAREFA",
                columns: table => new
                {
                    IDATUALIZACAOTAREFAS = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OBSERVACAO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DATAALTERACAO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CODIGOUSUARIO = table.Column<int>(type: "integer", nullable: false),
                    IDTAREFA = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATUALIZACAOTAREFA", x => x.IDATUALIZACAOTAREFAS);
                    table.ForeignKey(
                        name: "FK_ATUALIZACAOTAREFA_TAREFA_IDTAREFA",
                        column: x => x.IDTAREFA,
                        principalTable: "TAREFA",
                        principalColumn: "IDTAREFA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATUALIZACAOTAREFA_USUARIO_CODIGOUSUARIO",
                        column: x => x.CODIGOUSUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "IDUSUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMENTARIO",
                columns: table => new
                {
                    CODIGOCOMENTARIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TEXTO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IDTAREFA = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMENTARIO", x => x.CODIGOCOMENTARIO);
                    table.ForeignKey(
                        name: "FK_COMENTARIO_TAREFA_IDTAREFA",
                        column: x => x.IDTAREFA,
                        principalTable: "TAREFA",
                        principalColumn: "IDTAREFA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATUALIZACAOTAREFA_CODIGOUSUARIO",
                table: "ATUALIZACAOTAREFA",
                column: "CODIGOUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_ATUALIZACAOTAREFA_IDTAREFA",
                table: "ATUALIZACAOTAREFA",
                column: "IDTAREFA");

            migrationBuilder.CreateIndex(
                name: "IX_COMENTARIO_IDTAREFA",
                table: "COMENTARIO",
                column: "IDTAREFA");

            migrationBuilder.CreateIndex(
                name: "IX_TAREFA_IDPROJETO",
                table: "TAREFA",
                column: "IDPROJETO");

            migrationBuilder.CreateIndex(
                name: "IX_TAREFA_ProjetoId",
                table: "TAREFA",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATUALIZACAOTAREFA");

            migrationBuilder.DropTable(
                name: "COMENTARIO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "TAREFA");

            migrationBuilder.DropTable(
                name: "PROJETO");
        }
    }
}
