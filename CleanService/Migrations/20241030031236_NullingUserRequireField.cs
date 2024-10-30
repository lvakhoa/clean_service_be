using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanService.Migrations
{
    /// <inheritdoc />
    public partial class NullingUserRequireField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 943, DateTimeKind.Local).AddTicks(3221),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(3579));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 943, DateTimeKind.Local).AddTicks(1776),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(2906));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(9161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(9897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(6572),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(8896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(1452),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(994));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 948, DateTimeKind.Local).AddTicks(1798),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 435, DateTimeKind.Local).AddTicks(3017));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(1745),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(135),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(6441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(3782),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(3417),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(9801));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Complaints",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(6942),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 435, DateTimeKind.Local).AddTicks(1151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 946, DateTimeKind.Local).AddTicks(926),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(5455));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(9434),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(4734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 946, DateTimeKind.Local).AddTicks(6745),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(8147));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(9082),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 435, DateTimeKind.Local).AddTicks(1825));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(3579),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 943, DateTimeKind.Local).AddTicks(3221));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "PhoneNumber",
                keyValue: null,
                column: "PhoneNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(2906),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 943, DateTimeKind.Local).AddTicks(1776));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(9897),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(9161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(8896),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(6572));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(994),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(1452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 435, DateTimeKind.Local).AddTicks(3017),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 948, DateTimeKind.Local).AddTicks(1798));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(7070),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(1745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 433, DateTimeKind.Local).AddTicks(6441),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 944, DateTimeKind.Local).AddTicks(135));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(2146),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(3782));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(9801),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(3417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Complaints",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 435, DateTimeKind.Local).AddTicks(1151),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(6942));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(5455),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 946, DateTimeKind.Local).AddTicks(926));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(4734),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 945, DateTimeKind.Local).AddTicks(9434));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 434, DateTimeKind.Local).AddTicks(8147),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 946, DateTimeKind.Local).AddTicks(6745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 30, 1, 34, 24, 435, DateTimeKind.Local).AddTicks(1825),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 30, 10, 12, 34, 947, DateTimeKind.Local).AddTicks(9082));
        }
    }
}
