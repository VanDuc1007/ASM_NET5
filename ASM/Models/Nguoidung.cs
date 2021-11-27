using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Models
{
    public class Nguoidung
    {
        [Key]
        public int NguoidungID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Bạn cần nhập tài khoản.")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Bạn cần nhập họ tên.")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Chức danh")]
        public string Title { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Quản trị")]
        public bool Admin { get; set; }

        [Display(Name = "Sử dụng")]
        public bool Locked { get; set; }

        [Display(Name = "Mật khẩu")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Mật khẩu lần 2")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
