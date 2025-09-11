using System.ComponentModel.DataAnnotations;

namespace ADASOIdentityServer.AuthServer.Quickstart.Account
{
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage ="Şifre alanı boş olamaz")]
        public string Password { get; set; }

    }
}
