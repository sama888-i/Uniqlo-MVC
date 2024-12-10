using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.ViewModels.Auths
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password )]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
