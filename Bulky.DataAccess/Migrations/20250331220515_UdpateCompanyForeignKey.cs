using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UdpateCompanyForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompId",
                table: "AspNetUsers",
                column: "CompId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompId",
                table: "AspNetUsers",
                column: "CompId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
