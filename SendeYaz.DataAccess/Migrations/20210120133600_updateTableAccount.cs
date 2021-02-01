using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SendeYaz.DataAccess.Migrations
{
    public partial class updateTableAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhoto",
                table: "Accounts",
                maxLength: 255,
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                maxLength: 25,
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "Gsm",
                table: "Accounts",
                maxLength: 11,
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                maxLength: 25,
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                maxLength: 75,
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "space(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhoto",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "Gsm",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldMaxLength: 75,
                oldDefaultValueSql: "space(0)");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenExpiredDate", "RoleId" },
                values: new object[] { 1, 20, "lutfikotann@gmail.com", "Lütfi", "Kotan", new byte[] { 88, 104, 149, 252, 139, 208, 104, 75, 162, 145, 51, 40, 75, 216, 146, 131, 126, 182, 9, 51, 244, 156, 228, 243, 144, 201, 44, 37, 42, 101, 216, 106, 85, 119, 8, 114, 175, 252, 122, 201, 11, 116, 100, 76, 250, 133, 3, 96, 108, 53, 132, 177, 70, 61, 255, 135, 156, 223, 242, 92, 124, 105, 118, 46 }, new byte[] { 123, 130, 215, 198, 174, 130, 141, 113, 200, 64, 94, 141, 201, 167, 58, 144, 158, 62, 231, 184, 45, 8, 254, 206, 180, 0, 94, 177, 3, 147, 210, 124, 142, 41, 245, 252, 236, 14, 49, 235, 225, 213, 230, 118, 139, 6, 70, 191, 69, 46, 220, 181, 65, 96, 226, 186, 168, 247, 214, 244, 233, 114, 89, 214, 66, 79, 189, 51, 85, 99, 3, 1, 167, 65, 187, 237, 185, 168, 132, 218, 242, 193, 178, 117, 237, 205, 14, 107, 71, 91, 184, 214, 218, 179, 10, 13, 28, 120, 40, 51, 234, 61, 102, 87, 83, 162, 140, 166, 141, 112, 37, 116, 42, 65, 87, 192, 51, 115, 19, 149, 88, 53, 135, 39, 34, 21, 33, 50 }, "V80N2PP1BPUX2MOB3V7TUGRIXIICAICP7NAOKG5J4NC", new DateTime(2021, 1, 18, 23, 7, 40, 511, DateTimeKind.Local).AddTicks(2089), null });
        }
    }
}
