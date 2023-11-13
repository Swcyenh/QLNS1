using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS1.Models
{
    public class ThuTien
    {
        [Key]
        public int IdThuTien { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgayThu { get; set; }
        public int SoTienThu { get; set; }

    }
}
