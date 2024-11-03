using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanService.Migrations
{
    /// <inheritdoc />
    public partial class NullableDurationPriceInBookingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_DurationPrice_DurationPriceId",
                table: "BookingDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 593, DateTimeKind.Local).AddTicks(9998),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 111, DateTimeKind.Local).AddTicks(4311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 593, DateTimeKind.Local).AddTicks(8863),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 111, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(8179),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(7903));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(7137),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(5745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(9489),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(330));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(5938),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(7161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(4000),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(1927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(3398),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(620),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(2631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(2122),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Complaints",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(3583),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(3126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(4157),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(9757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(3483),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(8256));

            migrationBuilder.AlterColumn<Guid>(
                name: "DurationPriceId",
                table: "BookingDetails",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(8191),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 114, DateTimeKind.Local).AddTicks(6539));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(4491),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(4575));

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_DurationPrice_DurationPriceId",
                table: "BookingDetails",
                column: "DurationPriceId",
                principalTable: "DurationPrice",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_DurationPrice_DurationPriceId",
                table: "BookingDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 111, DateTimeKind.Local).AddTicks(4311),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 593, DateTimeKind.Local).AddTicks(9998));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 111, DateTimeKind.Local).AddTicks(2880),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 593, DateTimeKind.Local).AddTicks(8863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceTypes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(7903),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(8179));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceCategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(5745),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(7137));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomPricing",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(330),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(9489));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(7161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(5938));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(1927),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(4000));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Helpers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 112, DateTimeKind.Local).AddTicks(462),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 594, DateTimeKind.Local).AddTicks(3398));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "DurationPrice",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(2631),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(321),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(2122));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Complaints",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(3126),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(3583));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(9757),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(4157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 113, DateTimeKind.Local).AddTicks(8256),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(3483));

            migrationBuilder.AlterColumn<Guid>(
                name: "DurationPriceId",
                table: "BookingDetails",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 114, DateTimeKind.Local).AddTicks(6539),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 595, DateTimeKind.Local).AddTicks(8191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlacklistedAt",
                table: "BlacklistedUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 2, 19, 14, 0, 115, DateTimeKind.Local).AddTicks(4575),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 11, 2, 21, 27, 27, 596, DateTimeKind.Local).AddTicks(4491));

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_DurationPrice_DurationPriceId",
                table: "BookingDetails",
                column: "DurationPriceId",
                principalTable: "DurationPrice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
