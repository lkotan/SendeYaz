using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SendeYaz.DataAccess.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    IsBlocked = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    LastName = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    Email = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    Gsm = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    ProfilePhoto = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 255, nullable: false, defaultValueSql: "space(0)"),
                    RefreshTokenExpiredDate = table.Column<DateTime>(nullable: false, defaultValueSql: "Convert(Date,GetDate())"),
                    RoleId = table.Column<int>(nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ApplicationModule = table.Column<int>(nullable: false),
                    View = table.Column<bool>(nullable: false, defaultValue: false),
                    Insert = table.Column<bool>(nullable: false, defaultValue: false),
                    Update = table.Column<bool>(nullable: false, defaultValue: false),
                    Delete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    Description = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "space(0)"),
                    Content = table.Column<string>(nullable: false, defaultValueSql: "space(0)"),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "Convert(Date,GetDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTags_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenExpiredDate", "RoleId" },
                values: new object[] { 1, 20, "lutfikotann@gmail.com", "Lütfi", "Kotan", new byte[] { 88, 104, 149, 252, 139, 208, 104, 75, 162, 145, 51, 40, 75, 216, 146, 131, 126, 182, 9, 51, 244, 156, 228, 243, 144, 201, 44, 37, 42, 101, 216, 106, 85, 119, 8, 114, 175, 252, 122, 201, 11, 116, 100, 76, 250, 133, 3, 96, 108, 53, 132, 177, 70, 61, 255, 135, 156, 223, 242, 92, 124, 105, 118, 46 }, new byte[] { 123, 130, 215, 198, 174, 130, 141, 113, 200, 64, 94, 141, 201, 167, 58, 144, 158, 62, 231, 184, 45, 8, 254, 206, 180, 0, 94, 177, 3, 147, 210, 124, 142, 41, 245, 252, 236, 14, 49, 235, 225, 213, 230, 118, 139, 6, 70, 191, 69, 46, 220, 181, 65, 96, 226, 186, 168, 247, 214, 244, 233, 114, 89, 214, 66, 79, 189, 51, 85, 99, 3, 1, 167, 65, 187, 237, 185, 168, 132, 218, 242, 193, 178, 117, 237, 205, 14, 107, 71, 91, 184, 214, 218, 179, 10, 13, 28, 120, 40, 51, 234, 61, 102, 87, 83, 162, 140, 166, 141, 112, 37, 116, 42, 65, 87, 192, 51, 115, 19, 149, 88, 53, 135, 39, 34, 21, 33, 50 }, "V80N2PP1BPUX2MOB3V7TUGRIXIICAICP7NAOKG5J4NC", new DateTime(2021, 1, 18, 23, 7, 40, 511, DateTimeKind.Local).AddTicks(2089), null });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RefreshToken",
                table: "Accounts",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AccountId",
                table: "Blogs",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blogs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_BlogId",
                table: "BlogTags",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_TagId",
                table: "BlogTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_RoleId_ApplicationModule",
                table: "Rules",
                columns: new[] { "RoleId", "ApplicationModule" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
