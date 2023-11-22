using QLNS1.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLNS1.Models
{
    public class TonSachDauThang
    {
        [Key]
        public DateTime ThangNam { get; set; }
        public int SachTon { get; set; }

        }
    }

