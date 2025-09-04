using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercurialBackendDotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddedThirdPartyAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsThirdPartyUser",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsThirdPartyUser",
                table: "AspNetUsers");
        }
    }
}
