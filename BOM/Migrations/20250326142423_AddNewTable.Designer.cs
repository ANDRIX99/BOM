﻿// <auto-generated />
using System;
using BOM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BOM.Migrations
{
    [DbContext(typeof(BOMContext))]
    [Migration("20250326142423_AddNewTable")]
    partial class AddNewTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BOM.Model.DistintaBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("FiglioId")
                        .HasColumnType("int");

                    b.Property<int>("IdSon")
                        .HasColumnType("int");

                    b.Property<int>("IdVersion")
                        .HasColumnType("int");

                    b.Property<int>("VersioneDistintaBaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FiglioId");

                    b.HasIndex("VersioneDistintaBaseId");

                    b.ToTable("DistintaBase");
                });

            modelBuilder.Entity("BOM.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("BOM.Model.VersioneDistintaBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("VersioneDistintaBase");
                });

            modelBuilder.Entity("BOM.Model.DistintaBase", b =>
                {
                    b.HasOne("BOM.Model.Item", "Figlio")
                        .WithMany()
                        .HasForeignKey("FiglioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BOM.Model.VersioneDistintaBase", "VersioneDistintaBase")
                        .WithMany()
                        .HasForeignKey("VersioneDistintaBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Figlio");

                    b.Navigation("VersioneDistintaBase");
                });

            modelBuilder.Entity("BOM.Model.VersioneDistintaBase", b =>
                {
                    b.HasOne("BOM.Model.Item", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
