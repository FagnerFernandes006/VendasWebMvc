﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VendasWebMvc.Data;

namespace VendasWebMvc.Migrations
{
    [DbContext(typeof(VendasWebMvcContext))]
    partial class VendasWebMvcContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("VendasWebMvc.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmpresaId");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("VendasWebMvc.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CNPJ");

                    b.Property<string>("Nome");

                    b.Property<string>("NomeFantasia");

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("VendasWebMvc.Models.RegistroVendas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<double>("Qtde");

                    b.Property<int>("StatusVenda");

                    b.Property<int>("VendedorId");

                    b.HasKey("Id");

                    b.HasIndex("VendedorId");

                    b.ToTable("RegistroVendas");
                });

            modelBuilder.Entity("VendasWebMvc.Models.Vendedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataNascimento");

                    b.Property<int>("DepartamentoId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EmpresaId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<double>("SalarioBase");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("VendasWebMvc.Models.Departamento", b =>
                {
                    b.HasOne("VendasWebMvc.Models.Empresa", "Empresa")
                        .WithMany("Departamentos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VendasWebMvc.Models.RegistroVendas", b =>
                {
                    b.HasOne("VendasWebMvc.Models.Vendedor", "Vendedor")
                        .WithMany("Vendas")
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VendasWebMvc.Models.Vendedor", b =>
                {
                    b.HasOne("VendasWebMvc.Models.Departamento", "Departamento")
                        .WithMany("Vendedores")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VendasWebMvc.Models.Empresa", "Empresa")
                        .WithMany("Vendedores")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
