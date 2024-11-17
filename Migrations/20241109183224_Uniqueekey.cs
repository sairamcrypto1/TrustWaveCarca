using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustWaveCarca.Migrations
{
    /// <inheritdoc />
    public partial class Uniqueekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserLoginCredentialsId",
                table: "UserLoginCredentials",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginCredentials_UniqueLoginID",
                table: "UserLoginCredentials",
                column: "UniqueLoginID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserLoginCredentials_UniqueLoginID",
                table: "UserLoginCredentials");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserLoginCredentials",
                newName: "UserLoginCredentialsId");
        }
    }
}
