using System.ComponentModel.DataAnnotations;

namespace QLNS1.Models
{
    public class Sach
    {
        [Required]
        public int SachId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public string Picture { get; set; }
    }
}
