using System.ComponentModel.DataAnnotations;

namespace QLNS1.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public int MaHoaDon { get; set; }
        [Required]
        public string TenKhachHang { get; set; }
        [Required]
        public string sdt { get; set; }
        [Required]
        public string TenSach { get; set; }
        [Required]
        public string TheLoai { get; set; }
        public int SoLuong { get; set; }
        public int Gia { get; set; }
        public DateTime Date { get; set; }
        public int Debt { get; set; }

    }
}
