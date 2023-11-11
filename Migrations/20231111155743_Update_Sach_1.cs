using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_Sach_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sach_Nhap_NhapSachId",
                table: "Sach");

            migrationBuilder.DropIndex(
                name: "IX_Sach_NhapSachId",
                table: "Sach");

            migrationBuilder.DropColumn(
                name: "NhapSachId",
                table: "Sach");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhapSachId",
                table: "Sach",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sach_NhapSachId",
                table: "Sach",
                column: "NhapSachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sach_Nhap_NhapSachId",
                table: "Sach",
                column: "NhapSachId",
                principalTable: "Nhap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
