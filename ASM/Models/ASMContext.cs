using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class ASMContext :DbContext

    {
        public ASMContext(DbContextOptions<ASMContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<Nguoidung> Nguoidungs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonhangChitiet> DonhangChitiets { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
    }
}
