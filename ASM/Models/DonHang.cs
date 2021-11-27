using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Models
{
    public enum TrangthaiDonhang
    {
        [Display(Name = "Mới đặt")]
        Moidat=1,
        [Display(Name = "Đanggiao")]
        Danggiaot = 2,
        [Display(Name = "Đã giao")]
        Dagiao = 3,

    }
    public class DonHang
    {
        [Key]
        public int DonhangID { get; set; }

        [ForeignKey("Khachhang")]
        public int KhachhangID { get; set; }

        [Display(Name = "Ngày Đặt")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ngaydat { get; set; }

        [Required, Range(0,double.MaxValue, ErrorMessage ="Bạn cần nhập giá")]
        [Display(Name ="Tổng tiền")]
        public double Tongtien { get; set; }

        [Display(Name ="Trạng Thái")]
        public TrangthaiDonhang Trangthai { get; set; }

        [StringLength(250)]
        [Display(Name ="Ghi chú")]
        public string Ghichu { get; set; }
        public KhachHang Khachhang { get; set; }
        public List<DonhangChitiet> DonhangChitiets { get; set; }
    }
}
