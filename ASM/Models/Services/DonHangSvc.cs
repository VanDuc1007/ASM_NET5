using ASM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Services
{
    public interface IDonhangSvc
    {
        List<DonHang> GetDonhangAll();
        List<DonHang> GetDonhangbyKhachhang(int khachhangId);
        DonHang GetDonHang(int id);
        int AddDonhang(DonHang donhang);
        int EditDonhang(int id, DonHang donhang);
    }
    public class DonHangSvc : IDonhangSvc
    {
        protected ASMContext _context;
        public DonHangSvc(ASMContext context)
        {
            _context = context;
        }
        public List<DonHang> GetDonhangAll()
        {
            List<DonHang> list = new List<DonHang>();
            //kĩ thuật loding eager // từ khóa include
            list = _context.DonHangs.OrderByDescending(x => x.Ngaydat)
                .Include(x => x.Khachhang)
                .Include(x => x.DonhangChitiets)
                .ToList();
            return list;
        }
        public List<DonHang> GetDonhangbyKhachhang(int khachhangId)
        {
            List<DonHang> list = new List<DonHang>();
            list = _context.DonHangs.Where(x=>x.KhachhangID==khachhangId).OrderByDescending(x=>x.Ngaydat)
                .Include(x => x.Khachhang)
                .Include(x => x.DonhangChitiets)
                .ToList();
            return list;
        }
        public DonHang GetDonHang(int id)
        {
            DonHang donhang = null;
            donhang = _context.DonHangs.Where(x => x.DonhangID == id)
                .Include(x => x.Khachhang)
                .Include(x => x.DonhangChitiets).ThenInclude(y => y.MonAn)
                .FirstOrDefault();
            return donhang;
        }
        public int AddDonhang(DonHang donhang)
        {
            int ret = 0;
            try
            {
                _context.Add(donhang);
                _context.SaveChanges();
                ret = donhang.DonhangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditDonhang(int id , DonHang donhang)
        {
            int ret = 0;
            try
            {
                _context.Add(donhang);
                _context.SaveChanges();
                ret = donhang.DonhangID;
            }
            catch(Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
    }
}
