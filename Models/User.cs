using Microsoft.AspNetCore.Identity;

namespace Uniqlo2.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
    }
}
