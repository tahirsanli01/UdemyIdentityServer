using System.ComponentModel.DataAnnotations;

namespace ADASOIdentityServer.AuthServer.Quickstart.Account
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Email alanı zorunludur.")]        
        public string Email { get; set; }        
        public string Code { get; set; }
    }
}
