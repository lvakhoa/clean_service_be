using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanService.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreatePasswordColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(3230),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 588, DateTimeKind.Local).AddTicks(6921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(1660),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 588, DateTimeKind.Local).AddTicks(6066));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(5120),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(8205));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(2860),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(6399));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(6350),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(9637));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 871, DateTimeKind.Local).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(4751));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 871, DateTimeKind.Local).AddTicks(3510),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(7283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(7970),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(1428));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(7320),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(833));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(2120),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(5800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(1430),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(5187));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(9990),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(4042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(9490),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(3284));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(7680),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(839));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(8850),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(2544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(8420),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(2490),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(5136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(1820),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(5760),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(8854));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingContracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(7730),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(1456));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 871, DateTimeKind.Local).AddTicks(2250),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(5879));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 588, DateTimeKind.Local).AddTicks(6921),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(3230));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 588, DateTimeKind.Local).AddTicks(6066),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(8205),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(6399),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(2860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(9637),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(6350));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(4751),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 871, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(7283),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 871, DateTimeKind.Local).AddTicks(3510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(1428),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(833),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 868, DateTimeKind.Local).AddTicks(7320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(5800),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(2120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 589, DateTimeKind.Local).AddTicks(5187),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(1430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(4042),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(9990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(3284),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(9490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(839),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 869, DateTimeKind.Local).AddTicks(7680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(2544),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(2161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(8420));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(5136),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(2490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(1820));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 590, DateTimeKind.Local).AddTicks(8854),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(5760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingContracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(1456),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 870, DateTimeKind.Local).AddTicks(7730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 5, 14, 53, 29, 591, DateTimeKind.Local).AddTicks(5879),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 12, 19, 8, 45, 18, 871, DateTimeKind.Local).AddTicks(2250));
        }
    }
}
