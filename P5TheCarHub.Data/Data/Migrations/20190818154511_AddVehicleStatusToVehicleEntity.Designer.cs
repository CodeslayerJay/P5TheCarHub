﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P5TheCarHub.Infrastructure.Data;

namespace P5TheCarHub.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190818154511_AddVehicleStatusToVehicleEntity")]
    partial class AddVehicleStatusToVehicleEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerName")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("DateSold");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<decimal>("PriceSold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<bool>("IsMain")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Details")
                        .HasMaxLength(3000);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<DateTime?>("RepairDate");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Repairs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 7600.00m,
                            Description = "Full Restoration",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(4260),
                            VehicleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Cost = 350.00m,
                            Description = "Front wheel bearings",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5727),
                            VehicleId = 2
                        },
                        new
                        {
                            Id = 3,
                            Cost = 690.00m,
                            Description = "Radiator, brakes",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5759),
                            VehicleId = 3
                        },
                        new
                        {
                            Id = 4,
                            Cost = 1100.00m,
                            Description = "Tires, brakes",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5766),
                            VehicleId = 4
                        },
                        new
                        {
                            Id = 5,
                            Cost = 475.00m,
                            Description = "AC, Brakes",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5773),
                            VehicleId = 5
                        },
                        new
                        {
                            Id = 6,
                            Cost = 440.00m,
                            Description = "Tires",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5945),
                            VehicleId = 6
                        },
                        new
                        {
                            Id = 7,
                            Cost = 950.00m,
                            Description = "Tires, Brakes, AC",
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5954),
                            VehicleId = 7
                        });
                });

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableStatus");

                    b.Property<string>("Color")
                        .HasMaxLength(100);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<DateTime>("LotDate");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Mileage");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("PurchaseDate");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("SaleDate");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Trim")
                        .HasMaxLength(100);

                    b.Property<string>("VIN")
                        .HasMaxLength(500);

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableStatus = 2,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 425, DateTimeKind.Local).AddTicks(7469),
                            LotDate = new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Mazda",
                            Model = "Miata",
                            PurchaseDate = new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 1800.00m,
                            SaleDate = new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 9900.00m,
                            Trim = "LE",
                            Year = 1991
                        },
                        new
                        {
                            Id = 2,
                            AvailableStatus = 2,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(7890),
                            LotDate = new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Jeep",
                            Model = "Liberty",
                            PurchaseDate = new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 4500.00m,
                            SaleDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 5350.00m,
                            Trim = "Sport",
                            Year = 2007
                        },
                        new
                        {
                            Id = 3,
                            AvailableStatus = 0,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(7994),
                            LotDate = new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Dodge",
                            Model = "Grand Caravan",
                            PurchaseDate = new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 1800.00m,
                            SalePrice = 2990.00m,
                            Trim = "Sport",
                            Year = 2007
                        },
                        new
                        {
                            Id = 4,
                            AvailableStatus = 0,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8007),
                            LotDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Ford",
                            Model = "Explorer",
                            PurchaseDate = new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 24350.00m,
                            SalePrice = 25950.00m,
                            Trim = "XLT",
                            Year = 2017
                        },
                        new
                        {
                            Id = 5,
                            AvailableStatus = 2,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8016),
                            LotDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Honda",
                            Model = "Civic",
                            PurchaseDate = new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 4000.00m,
                            SaleDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 4975.00m,
                            Trim = "LX",
                            Year = 2008
                        },
                        new
                        {
                            Id = 6,
                            AvailableStatus = 2,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8026),
                            LotDate = new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Volkswagon",
                            Model = "GTI",
                            PurchaseDate = new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 15250.00m,
                            SaleDate = new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 16190.00m,
                            Trim = "S",
                            Year = 2016
                        },
                        new
                        {
                            Id = 7,
                            AvailableStatus = 2,
                            LastUpdated = new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8035),
                            LotDate = new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Ford",
                            Model = "Edge",
                            PurchaseDate = new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 10990.00m,
                            SaleDate = new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 12440.00m,
                            Trim = "SEL",
                            Year = 2013
                        });
                });

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Invoice", b =>
                {
                    b.HasOne("P5TheCarHub.Core.Entities.Vehicle", "Vehicle")
                        .WithOne("Invoice")
                        .HasForeignKey("P5TheCarHub.Core.Entities.Invoice", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Photo", b =>
                {
                    b.HasOne("P5TheCarHub.Core.Entities.Vehicle", "Vehicle")
                        .WithMany("Photos")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P5TheCarHub.Core.Entities.Repair", b =>
                {
                    b.HasOne("P5TheCarHub.Core.Entities.Vehicle", "Vehicle")
                        .WithMany("Repairs")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
