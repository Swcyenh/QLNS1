using Microsoft.AspNetCore.Identity;
namespace QLNS1.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get;set;}
        public int TienNo { get; set; }
    }
}
