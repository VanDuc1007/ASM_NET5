using ASM.Helpers;
using ASM.Models;
using ASM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Services
{
    public interface IKhachhangSvc
    {
        List<KhachHang> GetKhachHangAll();
        KhachHang GetKhachHang(int id);
        int AddKhachHang(KhachHang khachhang);
        int EditKhachHang(int id, KhachHang khachhang);
        KhachHang Login(ViewWebLogin viewWebLogin);
    }
    public class KhachhangSvc : IKhachhangSvc
    {
        protected ASMContext _context;
        protected IMahoaHelper _mahoaHelper;
        public KhachhangSvc(ASMContext context,IMahoaHelper mahoaHelper)
        {
            _context = context;
            _mahoaHelper = mahoaHelper;
        }
        public List<KhachHang> GetKhachHangAll()
        {
            List<KhachHang> list = new List<KhachHang>();
            list = _context.KhachHangs.ToList();
            return list;
        }
        public KhachHang GetKhachHang(int id)
        {
            KhachHang khachhang = null;
            khachhang = _context.KhachHangs.Find(id);
            return khachhang;
        }
        public int AddKhachHang(KhachHang khachhang)
        {
            int ret = 0;
            try
            {
                khachhang.Password = _mahoaHelper.Mahoa(khachhang.Password);
                khachhang.ConfirmPassword = khachhang.Password;

                _context.Add(khachhang);
                _context.SaveChanges();
                ret = khachhang.KhachhangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditKhachHang(int id, KhachHang khachhang)
        {
            int ret = 0;
            try
            {
                KhachHang _khachhang = null;
                _khachhang = _context.KhachHangs.Find(id); //cách này chỉ dùng cho Khóa chính

                _khachhang.FullName = _khachhang.FullName;
                _khachhang.Ngaysinh = _khachhang.Ngaysinh;
                _khachhang.PhoneNumber = _khachhang.PhoneNumber;
                _khachhang.EmailAddress = _khachhang.EmailAddress;
                if (_khachhang.Password != null)
                {
                    _khachhang.Password = _mahoaHelper.Mahoa(_khachhang.Password);
                    _khachhang.Password = _khachhang.Password;
                    _khachhang.ConfirmPassword = _khachhang.ConfirmPassword;
                }
                _khachhang.Mota = _khachhang.Mota;

                _context.Update(_khachhang);
                _context.SaveChanges();
                ret = _khachhang.KhachhangID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public KhachHang Login(ViewWebLogin viewWebLogin)
        {
            var u = _context.KhachHangs.Where(
                p => p.EmailAddress.Equals(viewWebLogin.Email)
                && p.Password.Equals(_mahoaHelper.Mahoa(viewWebLogin.Password))).FirstOrDefault();
            return u;
        }
    }
}
