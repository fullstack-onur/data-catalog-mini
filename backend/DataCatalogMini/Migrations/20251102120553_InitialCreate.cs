using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataCatalogMini.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TechnologyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeLogs_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Technologies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Oracle" },
                    { 2, "PostgreSQL" },
                    { 3, "Kafka" },
                    { 4, "SAP HANA" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "admin", "Admin", "admin" },
                    { 2, "1234", "User", "onur" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "LastUpdated", "Name", "Owner", "TechnologyId", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 28, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4515), "Customer_Records", "Data Governance", 1, "Table" },
                    { 2, new DateTime(2025, 10, 31, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4520), "Transactions_Stream", "Core Banking", 3, "Topic" },
                    { 3, new DateTime(2025, 11, 1, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4521), "HR_Employees", "HR Department", 2, "Table" }
                });

            migrationBuilder.InsertData(
                table: "ChangeLogs",
                columns: new[] { "Id", "AssetId", "ChangeDate", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 30, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4532), "Column added: customer_status", 1 },
                    { 2, 2, new DateTime(2025, 11, 1, 12, 5, 52, 924, DateTimeKind.Utc).AddTicks(4534), "New partition policy applied", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TechnologyId",
                table: "Assets",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_AssetId",
                table: "ChangeLogs",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_UserId",
                table: "ChangeLogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Technologies");
        }
    }
}
