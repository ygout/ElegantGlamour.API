﻿// <auto-generated />
using ElegantGlamour.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElegantGlamour.Data.Migrations
{
    [DbContext(typeof(ElegantGlamourDbContext))]
    [Migration("20200827145325_SeddDataMigration")]
    partial class SeddDataMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ElegantGlamour.Core.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Maquillage"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Soins"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Massage"
                        });
                });

            modelBuilder.Entity("ElegantGlamour.Core.Models.Prestation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Prestations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "ceci est la préstation numéro 1",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation1"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "ceci est la préstation numéro 2",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation2"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "ceci est la préstation numéro 3",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation3"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "ceci est la préstation numéro 4",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation1"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            Description = "ceci est la préstation numéro 5",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation1"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Description = "ceci est la préstation numéro 6",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation1"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 2,
                            Description = "ceci est la préstation numéro 7",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation1"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 3,
                            Description = "ceci est la préstation numéro 8",
                            Duration = 45,
                            Price = 30f,
                            Title = "Prestation1"
                        });
                });

            modelBuilder.Entity("ElegantGlamour.Core.Models.Prestation", b =>
                {
                    b.HasOne("ElegantGlamour.Core.Models.Category", "Category")
                        .WithMany("Prestations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
