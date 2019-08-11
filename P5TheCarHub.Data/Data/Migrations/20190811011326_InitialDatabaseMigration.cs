using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P5TheCarHub.Infrastructure.Data.Migrations
{
    public partial class InitialDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VIN = table.Column<string>(maxLength: 500, nullable: true),
                    Mileage = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Color = table.Column<string>(maxLength: 100, nullable: true),
                    Make = table.Column<string>(maxLength: 100, nullable: false),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    Trim = table.Column<string>(maxLength: 100, nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    LotDate = table.Column<DateTime>(nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleDate = table.Column<DateTime>(nullable: true),
                    IsSold = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceNumber = table.Column<string>(maxLength: 255, nullable: false),
                    PriceSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerName = table.Column<string>(maxLength: 200, nullable: true),
                    DateSold = table.Column<DateTime>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 1000, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    IsMain = table.Column<bool>(nullable: false, defaultValue: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(maxLength: 3000, nullable: true),
                    RepairDate = table.Column<DateTime>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Color", "IsSold", "LastUpdated", "LotDate", "Make", "Mileage", "Model", "PurchaseDate", "PurchasePrice", "SaleDate", "SalePrice", "Trim", "VIN", "Year" },
                values: new object[,]
                {
                    { 1, null, true, new DateTime(2019, 8, 10, 21, 13, 24, 399, DateTimeKind.Local).AddTicks(5916), new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mazda", null, "Miata", new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800.00m, new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 9900.00m, "LE", null, 1991 },
                    { 2, null, true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5088), new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jeep", null, "Liberty", new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4500.00m, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 5350.00m, "Sport", null, 2007 },
                    { 3, null, false, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5241), new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dodge", null, "Grand Caravan", new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800.00m, null, 2990.00m, "Sport", null, 2007 },
                    { 4, null, false, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5253), new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", null, "Explorer", new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 24350.00m, null, 25950.00m, "XLT", null, 2017 },
                    { 5, null, true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5533), new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda", null, "Civic", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000.00m, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4975.00m, "LX", null, 2008 },
                    { 6, null, true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5546), new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volkswagon", null, "GTI", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 15250.00m, new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 16190.00m, "S", null, 2016 },
                    { 7, null, true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5556), new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", null, "Edge", new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 10990.00m, new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12440.00m, "SEL", null, 2013 }
                });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "Id", "Cost", "Description", "Details", "LastUpdated", "RepairDate", "VehicleId" },
                values: new object[,]
                {
                    { 1, 7600.00m, "Full Restoration", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(4430), null, 1 },
                    { 2, 350.00m, "Front wheel bearings", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6354), null, 2 },
                    { 3, 690.00m, "Radiator, brakes", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6392), null, 3 },
                    { 4, 1100.00m, "Tires, brakes", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6400), null, 4 },
                    { 5, 475.00m, "AC, Brakes", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6410), null, 5 },
                    { 6, 440.00m, "Tires", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6420), null, 6 },
                    { 7, 950.00m, "Tires, Brakes, AC", null, new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6428), null, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VehicleId",
                table: "Invoices",
                column: "VehicleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_VehicleId",
                table: "Repairs",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
