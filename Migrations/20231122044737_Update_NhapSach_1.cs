using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_NhapSach_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TienDauThang",
                table: "TienDauThang");

            migrationBuilder.RenameTable(
                name: "TienDauThang",
                newName: "TonSachDauThang");

            migrationBuilder.AddColumn<int>(
                name: "SachSauNhap",
                table: "Nhap",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TonSachDauThang",
                table: "TonSachDauThang",
                column: "ThangNam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TonSachDauThang",
                table: "TonSachDauThang");

            migrationBuilder.DropColumn(
                name: "SachSauNhap",
                table: "Nhap");

            migrationBuilder.RenameTable(
                name: "TonSachDauThang",
                newName: "TienDauThang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TienDauThang",
                table: "TienDauThang",
                column: "ThangNam");
        }
    }
}
