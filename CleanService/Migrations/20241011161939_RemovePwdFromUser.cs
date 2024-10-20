using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanService.Migrations
{
    /// <inheritdoc />
    public partial class RemovePwdFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 11, 23, 19, 38, 761, DateTimeKind.Local).AddTicks(8220),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 6, 13, 1, 30, 86, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "HelperId",
                table: "RestrictedList",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "RestrictedList",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RestrictedList",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Helpers",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 11, 23, 19, 38, 762, DateTimeKind.Local).AddTicks(6088),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 6, 13, 1, 30, 87, DateTimeKind.Local).AddTicks(8504));

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingId",
                table: "Contracts",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "HelperId",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: true,
                comment: "Can be NULL if not assigned yet",
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldComment: "Can be NULL if not assigned yet")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 13, 1, 30, 86, DateTimeKind.Local).AddTicks(4323),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 11, 23, 19, 38, 761, DateTimeKind.Local).AddTicks(8220));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                comment: "Store hashed passwords")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "HelperId",
                table: "RestrictedList",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "RestrictedList",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "RestrictedList",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Helpers",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contracts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 13, 1, 30, 87, DateTimeKind.Local).AddTicks(8504),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 10, 11, 23, 19, 38, 762, DateTimeKind.Local).AddTicks(6088));

            migrationBuilder.AlterColumn<string>(
                name: "BookingId",
                table: "Contracts",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "HelperId",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: true,
                comment: "Can be NULL if not assigned yet",
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldComment: "Can be NULL if not assigned yet")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Bookings",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
