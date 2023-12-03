﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnionSa.Repository.Context;

#nullable disable

namespace OnionSa.Repository.Migrations
{
    [DbContext(typeof(OnionSaContext))]
    [Migration("20231203004839_SettingIdAgainMigration")]
    partial class SettingIdAgainMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnionSa.Domain.Models.Cliente", b =>
                {
                    b.Property<string>("CPFCNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("CPFCNPJ");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("OnionSa.Domain.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPFCNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumeroDoPedido")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CPFCNPJ");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("OnionSa.Domain.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdutoId"));

                    b.Property<int>("Preco")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ProdutoId");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            ProdutoId = 1,
                            Preco = 1000,
                            Titulo = "Celular"
                        },
                        new
                        {
                            ProdutoId = 2,
                            Preco = 3000,
                            Titulo = "Notebook"
                        },
                        new
                        {
                            ProdutoId = 3,
                            Preco = 5000,
                            Titulo = "Televisão"
                        });
                });

            modelBuilder.Entity("OnionSa.Domain.Models.Pedido", b =>
                {
                    b.HasOne("OnionSa.Domain.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("CPFCNPJ")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnionSa.Domain.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("OnionSa.Domain.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
