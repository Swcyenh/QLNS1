using System.ComponentModel.DataAnnotations;
namespace QLNS1.Models
{
    public class InvoiceDetail
    {
        [Required]
        public string InvoiceID { get; set; }
        [Required]
        public string SachID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
