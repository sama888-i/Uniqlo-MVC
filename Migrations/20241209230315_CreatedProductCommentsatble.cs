using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniqlo2.Migrations
{
    /// <inheritdoc />
    public partial class CreatedProductCommentsatble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_AspNetUsers_UserId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductRatings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductRatings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_AspNetUsers_UserId",
                table: "ProductRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_AspNetUsers_UserId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductRatings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductRatings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_AspNetUsers_UserId",
                table: "ProductRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
