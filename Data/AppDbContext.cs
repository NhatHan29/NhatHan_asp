using Microsoft.EntityFrameworkCore;
using nhathan_asp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using nhathan_asp.Models;
namespace nhathan_asp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ====== TABLES ======
        public DbSet<Student> Students { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<Diem> Diems { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<HocKy> HocKys { get; set; }
        public DbSet<DangKy> DangKys { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<PhanCong> PhanCongs { get; set; }

        // ====== CONFIG ======
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique MaSV
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.MaSV)
                .IsUnique();

            // Quan hệ Student - Lop (n-1)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Lop)
                .WithMany(l => l.Students)
                .HasForeignKey(s => s.LopId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ Diem
            modelBuilder.Entity<Diem>()
                .HasOne(d => d.Student)
                .WithMany(s => s.Diems)
                .HasForeignKey(d => d.StudentId);

            modelBuilder.Entity<Diem>()
                .HasOne(d => d.MonHoc)
                .WithMany(m => m.Diems)
                .HasForeignKey(d => d.MonHocId);

            // Quan hệ DangKy
            modelBuilder.Entity<DangKy>()
                .HasOne(dk => dk.Student)
                .WithMany()
                .HasForeignKey(dk => dk.StudentId);

            modelBuilder.Entity<DangKy>()
                .HasOne(dk => dk.HocKy)
                .WithMany(hk => hk.DangKys)
                .HasForeignKey(dk => dk.HocKyId);

            // Quan hệ PhanCong
            modelBuilder.Entity<PhanCong>()
                .HasOne(pc => pc.GiangVien)
                .WithMany()
                .HasForeignKey(pc => pc.GiangVienId);

            modelBuilder.Entity<PhanCong>()
                .HasOne(pc => pc.MonHoc)
                .WithMany()
                .HasForeignKey(pc => pc.MonHocId);
        }
    }
}