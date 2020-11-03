﻿// <auto-generated />
using ElegantGlamour.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElegantGlamour.Data.Migrations
{
    [DbContext(typeof(ElegantGlamourDbContext))]
    [Migration("20201103161025_NewlModel")]
    partial class NewlModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ElegantGlamour.Core.Models.Prestation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("PrestationCategoryId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("PrestationCategoryId");

                    b.ToTable("Prestations");
                });

            modelBuilder.Entity("ElegantGlamour.Core.Models.PrestationCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PrestationCategories");
                });

            modelBuilder.Entity("ElegantGlamour.Core.Models.Prestation", b =>
                {
                    b.HasOne("ElegantGlamour.Core.Models.PrestationCategory", "PrestationCategory")
                        .WithMany()
                        .HasForeignKey("PrestationCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
