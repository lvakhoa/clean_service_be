using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAutoGenOrderID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8692),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 722, DateTimeKind.Local).AddTicks(3433));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8021),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 722, DateTimeKind.Local).AddTicks(2158));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(8384),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 724, DateTimeKind.Local).AddTicks(1964));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(7161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(9564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(9501),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 724, DateTimeKind.Local).AddTicks(4158));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3733),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(9740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(6096),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 727, DateTimeKind.Local).AddTicks(4368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2841),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(1576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2165),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(287));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(6417),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(8113));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(5824),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(6885));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3193),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(8687));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2653),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(7635));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(631),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 724, DateTimeKind.Local).AddTicks(6456));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2175),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(6687));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(1589),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(5731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4778),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 725, DateTimeKind.Local).AddTicks(4320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4121),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 725, DateTimeKind.Local).AddTicks(2999));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(8141),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingContracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(844),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(4413));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(4855),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 727, DateTimeKind.Local).AddTicks(1853));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 722, DateTimeKind.Local).AddTicks(3433),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8692));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 722, DateTimeKind.Local).AddTicks(2158),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8021));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 724, DateTimeKind.Local).AddTicks(1964),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(8384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(9564),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(7161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 724, DateTimeKind.Local).AddTicks(4158),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(9501));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(9740),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 727, DateTimeKind.Local).AddTicks(4368),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(6096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(1576),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2841));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(287),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(8113),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(6417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 723, DateTimeKind.Local).AddTicks(6885),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(5824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(8687),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(7635),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 724, DateTimeKind.Local).AddTicks(6456),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(6687),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2175));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(5731),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(1589));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 725, DateTimeKind.Local).AddTicks(4320),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 725, DateTimeKind.Local).AddTicks(2999),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(466),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(8141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingContracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 726, DateTimeKind.Local).AddTicks(4413),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(844));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 12, 25, 22, 727, DateTimeKind.Local).AddTicks(1853),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(4855));
        }
    }
}
