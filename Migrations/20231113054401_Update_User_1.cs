using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_User_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NgayNo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayNo",
                table: "AspNetUsers");
        }
    }
}
