using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P5TheCarHub.Infrastructure.Data.Migrations
{
    public partial class AddVehicleStatusToVehicleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "AvailableStatus",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5727));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5759));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5766));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5773));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5945));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 434, DateTimeKind.Local).AddTicks(5954));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailableStatus", "LastUpdated" },
                values: new object[] { 2, new DateTime(2019, 8, 18, 11, 45, 8, 425, DateTimeKind.Local).AddTicks(7469) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableStatus", "LastUpdated" },
                values: new object[] { 2, new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8007));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvailableStatus", "LastUpdated" },
                values: new object[] { 2, new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8016) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AvailableStatus", "LastUpdated" },
                values: new object[] { 2, new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8026) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AvailableStatus", "LastUpdated" },
                values: new object[] { 2, new DateTime(2019, 8, 18, 11, 45, 8, 430, DateTimeKind.Local).AddTicks(8035) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableStatus",
                table: "Vehicles");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Vehicles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(4430));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6354));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6392));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6400));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6410));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "Id",
                keyValue: 7,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 412, DateTimeKind.Local).AddTicks(6428));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsSold", "LastUpdated" },
                values: new object[] { true, new DateTime(2019, 8, 10, 21, 13, 24, 399, DateTimeKind.Local).AddTicks(5916) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsSold", "LastUpdated" },
                values: new object[] { true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5088) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5241));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5253));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsSold", "LastUpdated" },
                values: new object[] { true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5533) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsSold", "LastUpdated" },
                values: new object[] { true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5546) });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsSold", "LastUpdated" },
                values: new object[] { true, new DateTime(2019, 8, 10, 21, 13, 24, 404, DateTimeKind.Local).AddTicks(5556) });
        }
    }
}
