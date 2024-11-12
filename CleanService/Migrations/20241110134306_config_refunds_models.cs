using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanService.Migrations
{
    /// <inheritdoc />
    public partial class config_refunds_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(652),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8692));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(162),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8021));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(7646),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(8384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(6851),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(7161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(8410),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(9501));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResolvedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(7952),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(9530),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(6096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(3568),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2841));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(3160),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(6327),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(6417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(5891),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(5824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(7477),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(7126),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(9154),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(6807),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2175));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(6485),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(1589));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(1851),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(1401),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(3939),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(8141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingContracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(5912),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(844));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(8702),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(4855));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8692),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(652));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 461, DateTimeKind.Local).AddTicks(8021),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(8384),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(7646));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(7161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(9501),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(8410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResolvedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Refunds",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3733),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(7952));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(6096),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2841),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(3568));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(2165),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(3160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(6417),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(6327));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HelperAvailability",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 462, DateTimeKind.Local).AddTicks(5824),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(5891));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(3193),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(7477));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2653),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(7126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(631),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 996, DateTimeKind.Local).AddTicks(9154));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(2175),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(6807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(1589),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4778),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(1851));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(4121),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(1401));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 463, DateTimeKind.Local).AddTicks(8141),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(3939));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingContracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(844),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(5912));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 10, 13, 13, 39, 464, DateTimeKind.Local).AddTicks(4855),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 10, 20, 43, 5, 997, DateTimeKind.Local).AddTicks(8702));
        }
    }
}
