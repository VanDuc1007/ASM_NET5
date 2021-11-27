using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachhangID { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "Bạn cần nhập họ tên.")]
        [Display(Name = "Họ và Tên")]
        
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Ngaysinh { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại."), Display(Name="Số phone")]
        [Column(TypeName = "varchar(15)"), MaxLength(15)]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]

        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu."), Display(Name = "Mật khẩu")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập lại mật khẩu."), Display(Name = "Mật khẩu (gõ lại)")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu gõ lại không khớp")]
        public string ConfirmPassword { get; set; }

        [StringLength(250)]
        [Display(Name= "Mô tả")]
        public string Mota { get; set; }

    }
}
