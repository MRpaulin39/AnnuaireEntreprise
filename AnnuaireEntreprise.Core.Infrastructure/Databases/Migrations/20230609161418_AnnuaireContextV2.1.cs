using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnnuaireEntreprise.Core.Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class AnnuaireContextV21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "F_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "F_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_F_Employee_ServiceId",
                table: "F_Employee",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_F_Employee_SiteId",
                table: "F_Employee",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_F_Employee_P_Service_ServiceId",
                table: "F_Employee",
                column: "ServiceId",
                principalTable: "P_Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_F_Employee_P_Site_SiteId",
                table: "F_Employee",
                column: "SiteId",
                principalTable: "P_Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_F_Employee_P_Service_ServiceId",
                table: "F_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_F_Employee_P_Site_SiteId",
                table: "F_Employee");

            migrationBuilder.DropIndex(
                name: "IX_F_Employee_ServiceId",
                table: "F_Employee");

            migrationBuilder.DropIndex(
                name: "IX_F_Employee_SiteId",
                table: "F_Employee");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "F_Employee");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "F_Employee");
        }
    }
}
