using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tChatLieu",
                columns: table => new
                {
                    MaChatLieu = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    ChatLieu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tChatLieu", x => x.MaChatLieu);
                });

            migrationBuilder.CreateTable(
                name: "tHangSX",
                columns: table => new
                {
                    MaHangSX = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    HangSX = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaNuocThuongHieu = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tHangSX", x => x.MaHangSX);
                });

            migrationBuilder.CreateTable(
                name: "tKichThuoc",
                columns: table => new
                {
                    MaKichThuoc = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    KichThuoc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tKichThuoc", x => x.MaKichThuoc);
                });

            migrationBuilder.CreateTable(
                name: "tLoaiDT",
                columns: table => new
                {
                    MaDT = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tLoaiDT", x => x.MaDT);
                });

            migrationBuilder.CreateTable(
                name: "tLoaiSP",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tLoaiSP", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "tMauSac",
                columns: table => new
                {
                    MaMauSac = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TenMauSac = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tMauSac", x => x.MaMauSac);
                });

            migrationBuilder.CreateTable(
                name: "tQuocGia",
                columns: table => new
                {
                    MaNuoc = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TenNuoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tQuocGia", x => x.MaNuoc);
                });

            migrationBuilder.CreateTable(
                name: "tTrangThai",
                columns: table => new
                {
                    MaTrangThai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tTrangThai", x => x.MaTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tDanhMucSP",
                columns: table => new
                {
                    MaSP = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MaChatLieu = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    NganLapTop = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    CanNang = table.Column<double>(type: "float", nullable: true),
                    DoNoi = table.Column<double>(type: "float", nullable: true),
                    MaHangSX = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    MaNuocSX = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Website = table.Column<string>(type: "varchar(155)", unicode: false, maxLength: 155, nullable: true),
                    ThoiGianBaoHanh = table.Column<double>(type: "float", nullable: true),
                    GioiThieuSP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ChietKhau = table.Column<double>(type: "float", nullable: true),
                    MaLoai = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    MaDT = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    GiaNhoNhat = table.Column<decimal>(type: "money", nullable: true),
                    GiaLonNhat = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tDanhMucSP", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tChatLieu_MaChatLieu",
                        column: x => x.MaChatLieu,
                        principalTable: "tChatLieu",
                        principalColumn: "MaChatLieu");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tHangSX_MaHangSX",
                        column: x => x.MaHangSX,
                        principalTable: "tHangSX",
                        principalColumn: "MaHangSX");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tLoaiDT_MaDT",
                        column: x => x.MaDT,
                        principalTable: "tLoaiDT",
                        principalColumn: "MaDT");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tLoaiSP_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "tLoaiSP",
                        principalColumn: "MaLoai");
                    table.ForeignKey(
                        name: "FK_tDanhMucSP_tQuocGia_MaNuocSX",
                        column: x => x.MaNuocSX,
                        principalTable: "tQuocGia",
                        principalColumn: "MaNuoc");
                });

            migrationBuilder.CreateTable(
                name: "tHoaDonBan",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayCan = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayGiao = table.Column<DateTime>(type: "datetime", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CachThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CachVanChuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhiVanChuyen = table.Column<double>(type: "float", nullable: false),
                    MaTrangThai = table.Column<int>(type: "int", nullable: false),
                    TongTienHD = table.Column<decimal>(type: "money", nullable: true),
                    GiamGiaHD = table.Column<double>(type: "float", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tHoaDonBan", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_tHoaDonBan_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tHoaDonBan_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tHoaDonBan_tTrangThai_MaTrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "tTrangThai",
                        principalColumn: "MaTrangThai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tAnhSP",
                columns: table => new
                {
                    MaSP = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TenFileAnh = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ViTri = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tAnhSP", x => new { x.MaSP, x.TenFileAnh });
                    table.ForeignKey(
                        name: "FK_tAnhSP_tDanhMucSP_MaSP",
                        column: x => x.MaSP,
                        principalTable: "tDanhMucSP",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tChiTietSanPham",
                columns: table => new
                {
                    MaChiTietSP = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    MaSP = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    MaKichThuoc = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    MaMauSac = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Video = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DonGiaBan = table.Column<double>(type: "float", nullable: true),
                    GiamGia = table.Column<double>(type: "float", nullable: true),
                    SLTon = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tChiTietSanPham", x => x.MaChiTietSP);
                    table.ForeignKey(
                        name: "FK_tChiTietSanPham_tDanhMucSP_MaSP",
                        column: x => x.MaSP,
                        principalTable: "tDanhMucSP",
                        principalColumn: "MaSP");
                    table.ForeignKey(
                        name: "FK_tChiTietSanPham_tKichThuoc_MaKichThuoc",
                        column: x => x.MaKichThuoc,
                        principalTable: "tKichThuoc",
                        principalColumn: "MaKichThuoc");
                    table.ForeignKey(
                        name: "FK_tChiTietSanPham_tMauSac_MaMauSac",
                        column: x => x.MaMauSac,
                        principalTable: "tMauSac",
                        principalColumn: "MaMauSac");
                });

            migrationBuilder.CreateTable(
                name: "tChiTietHDB",
                columns: table => new
                {
                    MaChiTietHDB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DonGiaBan = table.Column<decimal>(type: "money", nullable: true),
                    SoLuongBan = table.Column<int>(type: "int", nullable: true),
                    GiamGia = table.Column<double>(type: "float", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tChiTietHDB", x => x.MaChiTietHDB);
                    table.ForeignKey(
                        name: "FK_tChiTietHDB_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tChiTietHDB_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tChiTietHDB_tDanhMucSP_MaSP",
                        column: x => x.MaSP,
                        principalTable: "tDanhMucSP",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tChiTietHDB_tHoaDonBan_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "tHoaDonBan",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tAnhChiTietSP",
                columns: table => new
                {
                    MaChiTietSP = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TenFileAnh = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ViTri = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tAnhChiTietSP", x => new { x.MaChiTietSP, x.TenFileAnh });
                    table.ForeignKey(
                        name: "FK_tAnhChiTietSP_tChiTietSanPham_MaChiTietSP",
                        column: x => x.MaChiTietSP,
                        principalTable: "tChiTietSanPham",
                        principalColumn: "MaChiTietSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietHDB_ApplicationUserId",
                table: "tChiTietHDB",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietHDB_MaHoaDon",
                table: "tChiTietHDB",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietHDB_MaSP",
                table: "tChiTietHDB",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietHDB_UserId",
                table: "tChiTietHDB",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietSanPham_MaKichThuoc",
                table: "tChiTietSanPham",
                column: "MaKichThuoc");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietSanPham_MaMauSac",
                table: "tChiTietSanPham",
                column: "MaMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_tChiTietSanPham_MaSP",
                table: "tChiTietSanPham",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaChatLieu",
                table: "tDanhMucSP",
                column: "MaChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaDT",
                table: "tDanhMucSP",
                column: "MaDT");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaHangSX",
                table: "tDanhMucSP",
                column: "MaHangSX");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaLoai",
                table: "tDanhMucSP",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_tDanhMucSP_MaNuocSX",
                table: "tDanhMucSP",
                column: "MaNuocSX");

            migrationBuilder.CreateIndex(
                name: "IX_tHoaDonBan_ApplicationUserId",
                table: "tHoaDonBan",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tHoaDonBan_MaTrangThai",
                table: "tHoaDonBan",
                column: "MaTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_tHoaDonBan_UserId",
                table: "tHoaDonBan",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tAnhChiTietSP");

            migrationBuilder.DropTable(
                name: "tAnhSP");

            migrationBuilder.DropTable(
                name: "tChiTietHDB");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "tChiTietSanPham");

            migrationBuilder.DropTable(
                name: "tHoaDonBan");

            migrationBuilder.DropTable(
                name: "tDanhMucSP");

            migrationBuilder.DropTable(
                name: "tKichThuoc");

            migrationBuilder.DropTable(
                name: "tMauSac");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tTrangThai");

            migrationBuilder.DropTable(
                name: "tChatLieu");

            migrationBuilder.DropTable(
                name: "tHangSX");

            migrationBuilder.DropTable(
                name: "tLoaiDT");

            migrationBuilder.DropTable(
                name: "tLoaiSP");

            migrationBuilder.DropTable(
                name: "tQuocGia");
        }
    }
}
