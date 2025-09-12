using System.ComponentModel.DataAnnotations;

namespace ADASOIdentityServer.AuthServer.Quickstart.Account
{
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage ="Şifre alanı boş olamaz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar alanı boş olamaz")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }

    }
}
