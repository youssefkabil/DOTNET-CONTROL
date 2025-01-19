using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_Control.Migrations
{
    /// <inheritdoc />
    public partial class addFavUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-aaaa-bbbb-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba114cd7-ddd5-4be6-b232-5e59aefa9b68", "AQAAAAIAAYagAAAAEPaI89ur5nUBpbPqLFIZKcKomMdkVLKiGCAErcNF/mS7YNtp5Oyrnf4yYajtXkChnQ==" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "images/book1.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "images/book1.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "images/book1.jpg");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ApplicationUserId",
                table: "Favorites",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_ApplicationUserId",
                table: "Favorites",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_ApplicationUserId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_ApplicationUserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Favorites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-aaaa-bbbb-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64acdcb8-64fd-4d52-adee-e7ada64e3227", "AQAAAAIAAYagAAAAEA6dcc9ZemUitihwjvqk3iQYu6nAga42UiqZ+8jFW9qjcND4mi2l6cG/h3fpt0gT/A==" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "");
        }
    }
}
