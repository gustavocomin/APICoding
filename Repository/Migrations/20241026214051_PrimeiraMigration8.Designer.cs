﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Config.Db;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241026214051_PrimeiraMigration8")]
    partial class PrimeiraMigration8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("Domain.Projetos.Models.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PROJETO", (string)null);
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Atualizacoes.Models.AtualizacaoTarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("CodigoUsuario")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("DATE");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("IdTarefa")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CodigoUsuario");

                    b.HasIndex("IdTarefa");

                    b.ToTable("ATUALIZACAOTAREFA", (string)null);
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Comentarios.Models.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("IdComentario")
                        .HasColumnType("integer");

                    b.Property<int>("IdTarefa")
                        .HasColumnType("integer");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.HasIndex("IdTarefa");

                    b.ToTable("COMENTARIO", (string)null);
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Models.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("IdProjeto")
                        .HasColumnType("integer");

                    b.Property<int>("Prioridade")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdProjeto");

                    b.ToTable("TAREFA", (string)null);
                });

            modelBuilder.Entity("Domain.Usuarios.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("IDUSUARIO");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("Acesso")
                        .HasColumnType("integer")
                        .HasColumnName("ACESSO");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("LOGIN");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.ToTable("USUARIO", (string)null);
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Atualizacoes.Models.AtualizacaoTarefa", b =>
                {
                    b.HasOne("Domain.Usuarios.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("CodigoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Projetos.Tarefas.Models.Tarefa", "Tarefa")
                        .WithMany("AtualizacaoTarefa")
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Comentarios.Models.Comentario", b =>
                {
                    b.HasOne("Domain.Projetos.Tarefas.Models.Tarefa", "Tarefa")
                        .WithMany("Comentarios")
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Models.Tarefa", b =>
                {
                    b.HasOne("Domain.Projetos.Models.Projeto", "Projeto")
                        .WithMany("Tarefas")
                        .HasForeignKey("IdProjeto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projeto");
                });

            modelBuilder.Entity("Domain.Projetos.Models.Projeto", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("Domain.Projetos.Tarefas.Models.Tarefa", b =>
                {
                    b.Navigation("AtualizacaoTarefa");

                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
