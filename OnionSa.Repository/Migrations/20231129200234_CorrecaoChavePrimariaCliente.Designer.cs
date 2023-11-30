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
    [Migration("20231129200234_CorrecaoChavePrimariaCliente")]
    partial class CorrecaoChavePrimariaCliente
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
                    b.Property<long>("CPFCNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("bigint");

                    b.Property<int>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("int");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("CPFCNPJ");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("OnionSa.Domain.Models.Pedido", b =>
                {
                    b.Property<int>("NumeroDoPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NumeroDoPedido"));

                    b.Property<long>("CPFCNPJ")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("NumeroDoPedido");

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
