﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATUALIZACAOTAREFA_TAREFA_TarefaId",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.DropForeignKey(
                name: "FK_COMENTARIO_TAREFA_TarefaId",
                table: "COMENTARIO");

            migrationBuilder.DropForeignKey(
                name: "FK_TAREFA_PROJETO_ProjetoId",
                table: "TAREFA");

            migrationBuilder.DropIndex(
                name: "IX_TAREFA_ProjetoId",
                table: "TAREFA");

            migrationBuilder.DropIndex(
                name: "IX_COMENTARIO_TarefaId",
                table: "COMENTARIO");

            migrationBuilder.DropIndex(
                name: "IX_ATUALIZACAOTAREFA_TarefaId",
                table: "ATUALIZACAOTAREFA");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "TAREFA");

            migrationBuilder.DropColumn(
                name: "TarefaId",
                table: "COMENTARIO");

            migrationBuilder.DropColumn(
                name: "TarefaId",
                table: "ATUALIZACAOTAREFA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "TAREFA",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarefaId",
                table: "COMENTARIO",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarefaId",
                table: "ATUALIZACAOTAREFA",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TAREFA_ProjetoId",
                table: "TAREFA",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_COMENTARIO_TarefaId",
                table: "COMENTARIO",
                column: "TarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_ATUALIZACAOTAREFA_TarefaId",
                table: "ATUALIZACAOTAREFA",
                column: "TarefaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATUALIZACAOTAREFA_TAREFA_TarefaId",
                table: "ATUALIZACAOTAREFA",
                column: "TarefaId",
                principalTable: "TAREFA",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_COMENTARIO_TAREFA_TarefaId",
                table: "COMENTARIO",
                column: "TarefaId",
                principalTable: "TAREFA",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TAREFA_PROJETO_ProjetoId",
                table: "TAREFA",
                column: "ProjetoId",
                principalTable: "PROJETO",
                principalColumn: "Id");
        }
    }
}