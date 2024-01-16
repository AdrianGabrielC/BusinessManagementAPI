using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlanzCleanAPI.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeWorkPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e810fe9-2096-4e5f-9d6f-4598f0a59d68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50442760-0f89-40e5-8b09-c5e85f91c6d2");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "EmployeeWorks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7582c2a2-2521-4744-bbc0-2ad254bbc12f", null, "Admin", "ADMIN" },
                    { "c61b1911-f02c-49fe-9dcf-b62e6fc4b835", null, "Employee", "EMPLOYEE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7582c2a2-2521-4744-bbc0-2ad254bbc12f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c61b1911-f02c-49fe-9dcf-b62e6fc4b835");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "EmployeeWorks");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e810fe9-2096-4e5f-9d6f-4598f0a59d68", null, "Admin", "ADMIN" },
                    { "50442760-0f89-40e5-8b09-c5e85f91c6d2", null, "Employee", "EMPLOYEE" }
                });
        }
    }
}
