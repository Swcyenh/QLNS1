using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS1.Models
{
    public class NhapSach
    {
        public int Id { get; set; }
        [ForeignKey("Sach")]
        [Required(ErrorMessage = "Vui lòng nhập mã sách")]
        public int SachId { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string TheLoai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng nhập")]
        public int AmountImport { get; set; }

        public DateTime DateImport { get; set; }
    }
}
