﻿// <auto-generated />
using System;
using BancoAPI.SA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BancoAPI.SA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BancoAPI.SA.Models.ClienteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("BancoAPI.SA.Models.ContaCorrenteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Agencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("ContaCorrentes");
                });

            modelBuilder.Entity("BancoAPI.SA.Models.ExtratoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumroContaId")
                        .HasColumnType("int");

                    b.Property<decimal>("SaldoFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SaldoInicial")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("contaCorrenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("contaCorrenteId");

                    b.ToTable("Extratos");
                });

            modelBuilder.Entity("BancoAPI.SA.Models.TipoTransacaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoTransacao");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Depósito"
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Saque"
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Transferência"
                        });
                });

            modelBuilder.Entity("BancoAPI.SA.Models.TransacaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataTransacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroContaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoTransacaoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("contaCorrenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoTransacaoId");

                    b.HasIndex("contaCorrenteId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("BancoAPI.SA.Models.ContaCorrenteModel", b =>
                {
                    b.HasOne("BancoAPI.SA.Models.ClienteModel", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BancoAPI.SA.Models.ExtratoModel", b =>
                {
                    b.HasOne("BancoAPI.SA.Models.ContaCorrenteModel", "contaCorrente")
                        .WithMany()
                        .HasForeignKey("contaCorrenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("contaCorrente");
                });

            modelBuilder.Entity("BancoAPI.SA.Models.TransacaoModel", b =>
                {
                    b.HasOne("BancoAPI.SA.Models.TipoTransacaoModel", "tipoTransacao")
                        .WithMany()
                        .HasForeignKey("TipoTransacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BancoAPI.SA.Models.ContaCorrenteModel", "contaCorrente")
                        .WithMany()
                        .HasForeignKey("contaCorrenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("contaCorrente");

                    b.Navigation("tipoTransacao");
                });
#pragma warning restore 612, 618
        }
    }
}
