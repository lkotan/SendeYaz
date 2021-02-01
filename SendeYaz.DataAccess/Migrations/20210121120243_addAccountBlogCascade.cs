using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SendeYaz.DataAccess.Migrations
{
    public partial class addAccountBlogCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "Blogs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AccountId",
                table: "Likes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BlogId",
                table: "Likes",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Blogs");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenExpiredDate", "RoleId" },
                values: new object[] { 1, 20, "lutfikotann@gmail.com", "Lütfi", "Kotan", new byte[] { 13, 244, 43, 139, 78, 35, 210, 31, 178, 184, 193, 128, 86, 240, 203, 183, 60, 230, 204, 124, 157, 214, 17, 227, 222, 215, 94, 221, 232, 74, 88, 85, 148, 1, 29, 6, 151, 17, 144, 226, 219, 217, 192, 82, 210, 1, 150, 85, 144, 121, 227, 0, 238, 155, 39, 34, 85, 254, 27, 69, 124, 230, 72, 46 }, new byte[] { 220, 159, 233, 235, 49, 0, 133, 2, 237, 107, 41, 227, 9, 50, 255, 181, 221, 58, 220, 148, 223, 157, 208, 89, 149, 215, 75, 136, 213, 206, 63, 9, 127, 169, 115, 204, 30, 224, 77, 89, 154, 83, 17, 104, 108, 78, 145, 240, 60, 211, 63, 178, 93, 158, 234, 215, 18, 182, 226, 75, 135, 0, 129, 187, 107, 204, 156, 236, 65, 87, 187, 12, 178, 68, 122, 220, 117, 110, 249, 224, 150, 152, 218, 235, 71, 220, 207, 158, 142, 5, 99, 122, 113, 10, 15, 195, 225, 86, 178, 6, 223, 194, 122, 156, 58, 52, 121, 191, 77, 147, 5, 30, 136, 167, 51, 191, 68, 255, 184, 244, 84, 92, 115, 140, 126, 103, 33, 106 }, "WH9B93S9WF7DF8GFBVVHMVESWLMFOYR31GLT3TZK", new DateTime(2021, 1, 19, 16, 38, 37, 796, DateTimeKind.Local).AddTicks(7938), null });
        }
    }
}
