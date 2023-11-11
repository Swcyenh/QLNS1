using Microsoft.AspNetCore.Identity;
namespace QLNS1.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
