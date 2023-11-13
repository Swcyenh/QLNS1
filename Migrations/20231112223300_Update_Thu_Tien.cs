﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_Thu_Tien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ThuTien",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ThuTien_Email",
                table: "ThuTien",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_ThuTien_AspNetUsers_Email",
                table: "ThuTien",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThuTien_AspNetUsers_Email",
                table: "ThuTien");

            migrationBuilder.DropIndex(
                name: "IX_ThuTien_Email",
                table: "ThuTien");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ThuTien",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
