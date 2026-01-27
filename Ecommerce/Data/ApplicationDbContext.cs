using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TAnhChiTietSp> TAnhChiTietSps { get; set; }
        public DbSet<TAnhSp> TAnhSps { get; set; }
        public DbSet<TChatLieu> TChatLieus { get; set; }
        public DbSet<TChiTietHdb> TChiTietHdbs { get; set; }
        public DbSet<TChiTietSanPham> TChiTietSanPhams { get; set; }
        public DbSet<TDanhMucSp> TDanhMucSps { get; set; }
        public DbSet<THangSx> THangSxes { get; set; }
        public DbSet<THoaDonBan> THoaDonBans { get; set; }
        public DbSet<TKichThuoc> TKichThuocs { get; set; }
        public DbSet<TLoaiDt> TLoaiDts { get; set; }
        public DbSet<TLoaiSp> TLoaiSps { get; set; }
        public DbSet<TMauSac> TMauSacs { get; set; }
        public DbSet<TQuocGia> TQuocGias { get; set; }
        public DbSet<TTrangThai> TTrangThais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // QUAN HỆ: THoaDonBan - IdentityUser
            modelBuilder.Entity<THoaDonBan>()
                .HasOne(h => h.UserNameNavigation)
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // QUAN HỆ: THoaDonBan - TTrangThai (1 trạng thái có nhiều hóa đơn)
            modelBuilder.Entity<THoaDonBan>()
                .HasOne(h => h.MaTrangThaiNavigation)
                .WithMany(t => t.THoaDonBans)
                .HasForeignKey(h => h.MaTrangThai)
                .OnDelete(DeleteBehavior.Restrict);

            // QUAN HỆ: TChiTietHdb - THoaDonBan (1 hóa đơn có nhiều chi tiết)
            modelBuilder.Entity<TChiTietHdb>()
                .HasOne(ct => ct.MaHoaDonNavigation)
                .WithMany(h => h.TChiTietHdbs)
                .HasForeignKey(ct => ct.MaHoaDon)
                .OnDelete(DeleteBehavior.Cascade);

            // QUAN HỆ: TChiTietHdb - TDanhMucSp (1 sản phẩm có nhiều chi tiết hóa đơn)
            modelBuilder.Entity<TChiTietHdb>()
                .HasOne(ct => ct.MaSpNavigation)
                .WithMany(sp => sp.TChiTietHdbs)
                .HasForeignKey(ct => ct.MaSp)
                .OnDelete(DeleteBehavior.Restrict);

            // QUAN HỆ: TChiTietHdb - IdentityUser
            modelBuilder.Entity<TChiTietHdb>()
                .HasOne(ct => ct.UserNameNavigation)
                .WithMany()
                .HasForeignKey(ct => ct.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
