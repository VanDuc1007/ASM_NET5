using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM.Migrations
{
    public partial class ASM3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    KhachhangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 150, nullable: false),
                    Ngaysinh = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Mota = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.KhachhangID);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonhangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachhangID = table.Column<int>(nullable: false),
                    Ngaydat = table.Column<DateTime>(nullable: false),
                    Tongtien = table.Column<double>(nullable: false),
                    Trangthai = table.Column<bool>(nullable: false),
                    Ghichu = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonhangID);
                    table.ForeignKey(
                        name: "FK_DonHangs_KhachHangs_KhachhangID",
                        column: x => x.KhachhangID,
                        principalTable: "KhachHangs",
                        principalColumn: "KhachhangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonhangChitiets",
                columns: table => new
                {
                    ChitietID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonhangID = table.Column<int>(nullable: false),
                    MonAnID = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    Thanhtien = table.Column<double>(nullable: false),
                    Ghichu = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonhangChitiets", x => x.ChitietID);
                    table.ForeignKey(
                        name: "FK_DonhangChitiets_DonHangs_DonhangID",
                        column: x => x.DonhangID,
                        principalTable: "DonHangs",
                        principalColumn: "DonhangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonhangChitiets_MonAns_MonAnID",
                        column: x => x.MonAnID,
                        principalTable: "MonAns",
                        principalColumn: "MonAnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonhangChitiets_DonhangID",
                table: "DonhangChitiets",
                column: "DonhangID");

            migrationBuilder.CreateIndex(
                name: "IX_DonhangChitiets_MonAnID",
                table: "DonhangChitiets",
                column: "MonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_KhachhangID",
                table: "DonHangs",
                column: "KhachhangID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonhangChitiets");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");
        }
    }
}
