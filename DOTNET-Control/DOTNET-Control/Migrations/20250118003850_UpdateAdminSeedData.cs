using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_Control.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-aaaa-bbbb-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "289e5afd-e9fc-4b47-849a-69f1b9736765", "KABIL YOUSSEF", "AQAAAAIAAYagAAAAEG+phoAaG7dVoTmMg/fpe3CiBZwm+Qib8zbBEz2aBKLds5gfbtI/Bk5vxGhXduwBRw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-aaaa-bbbb-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "f28cb31d-fe48-466c-943b-9f38d2e9c50c", "ADMIN@LIBRARY.COM", "AQAAAAIAAYagAAAAEHBqh6XvcS+tqEmoSSAQnSpOGFWXpT09xVuc6j6UoLgmnCXKCWP/dfiyCtkc+qx5Cg==" });
        }
    }
}
