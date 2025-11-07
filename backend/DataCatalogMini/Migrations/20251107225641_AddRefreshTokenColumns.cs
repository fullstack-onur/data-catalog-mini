using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataCatalogMini.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiresAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 11, 2, 22, 56, 41, 419, DateTimeKind.Utc).AddTicks(8999));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 11, 5, 22, 56, 41, 419, DateTimeKind.Utc).AddTicks(9011));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 11, 6, 22, 56, 41, 419, DateTimeKind.Utc).AddTicks(9013));

            migrationBuilder.UpdateData(
                table: "ChangeLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChangeDate",
                value: new DateTime(2025, 11, 4, 22, 56, 41, 419, DateTimeKind.Utc).AddTicks(9044));

            migrationBuilder.UpdateData(
                table: "ChangeLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChangeDate",
                value: new DateTime(2025, 11, 6, 22, 56, 41, 419, DateTimeKind.Utc).AddTicks(9047));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RefreshToken", "RefreshTokenExpiresAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RefreshToken", "RefreshTokenExpiresAt" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresAt",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 10, 28, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4515));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 10, 31, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 11, 1, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "ChangeLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChangeDate",
                value: new DateTime(2025, 10, 30, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "ChangeLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChangeDate",
                value: new DateTime(2025, 11, 1, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4534));
        }
    }
}
