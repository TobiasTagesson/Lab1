﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsService.Models;

namespace ProductsService.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20200708172212_AlterDataSeed")]
    partial class AlterDataSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductsService.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"),
                            Description = "Racquet for the advanced player",
                            ImageUrl = "/images/WilsonProstaff.jpg",
                            Name = "Wilson Pro Staff 97",
                            Price = 199.00m
                        },
                        new
                        {
                            Id = new Guid("04259d6e-d326-4d40-8936-cd85e688bab4"),
                            Description = "Racquet with great control",
                            ImageUrl = "/images/Dunlop.jpg",
                            Name = "Dunlop Biomimetic 300",
                            Price = 179.00m
                        },
                        new
                        {
                            Id = new Guid("7dec6022-2f61-47cb-8bd0-8bd9e35680dd"),
                            Description = "Great power, great control",
                            ImageUrl = "/images/Yonex.jpg",
                            Name = "Yonex DR 98",
                            Price = 189.00m
                        },
                        new
                        {
                            Id = new Guid("2559ce81-66f4-46f4-a924-8254fd188889"),
                            Description = "Classic control racquet",
                            ImageUrl = "/images/Head.jpg",
                            Name = "Head Prestige MP",
                            Price = 209.00m
                        },
                        new
                        {
                            Id = new Guid("3a4a7aec-8119-4700-bd81-b22f79089ec1"),
                            Description = "Modern feel",
                            ImageUrl = "/images/WilsonBlade.jpg",
                            Name = "Wilson Blade 98",
                            Price = 199.00m
                        },
                        new
                        {
                            Id = new Guid("fe9fee5a-5792-49d4-a146-09e6961b1863"),
                            Description = "Spin friendly racquet",
                            ImageUrl = "/images/Babolat.jpg",
                            Name = "Babolat Pure Aero",
                            Price = 169.00m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
