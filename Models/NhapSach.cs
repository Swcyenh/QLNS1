using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS1.Models
{
    public class NhapSach
    {
        public int Id { get; set; }
        [Required]
        public string TenSach { get; set; }
        [Required]
        public string TacGia { get; set; }
        [Required]
        public string TheLoai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng nhập")]
        public int AmountImport { get; set; }

        public DateTime DateImport { get; set; }
        public int SachSauNhap { get; set; }
    }
}
