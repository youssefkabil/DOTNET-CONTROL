using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_Control.Migrations
{
    /// <inheritdoc />
    public partial class hebla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-aaaa-bbbb-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "08ffd27f-3340-442e-9672-b0ca6dd3aa87", "AQAAAAIAAYagAAAAEJNzJDYLXfU1E4BDQoL3vrWitw6E8SC5HibT/YK3XFVRBwL2IWMVlXooTNmCukb8rg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-aaaa-bbbb-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba114cd7-ddd5-4be6-b232-5e59aefa9b68", "AQAAAAIAAYagAAAAEPaI89ur5nUBpbPqLFIZKcKomMdkVLKiGCAErcNF/mS7YNtp5Oyrnf4yYajtXkChnQ==" });
        }
    }
}
