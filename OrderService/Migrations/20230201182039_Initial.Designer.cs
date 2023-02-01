﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.Database;

#nullable disable

namespace OrderService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230201182039_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderService.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = "C-1",
                            OrderDate = new DateTime(2023, 2, 1, 19, 20, 39, 48, DateTimeKind.Local).AddTicks(213),
                            Price = 100m,
                            ProductId = "P-1",
                            Quantity = 20,
                            Total = 2000m
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = "C-2",
                            OrderDate = new DateTime(2023, 2, 1, 19, 20, 39, 48, DateTimeKind.Local).AddTicks(275),
                            Price = 30m,
                            ProductId = "P-2",
                            Quantity = 15,
                            Total = 450m
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = "C-3",
                            OrderDate = new DateTime(2023, 2, 1, 19, 20, 39, 48, DateTimeKind.Local).AddTicks(279),
                            Price = 50m,
                            ProductId = "P-1",
                            Quantity = 10,
                            Total = 500m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
