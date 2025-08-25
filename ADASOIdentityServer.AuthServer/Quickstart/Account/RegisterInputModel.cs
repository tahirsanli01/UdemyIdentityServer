using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI;
public class RegisterInputModel
{
    // Kullanıcının dolduracağı alanlar
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [StringLength(80, ErrorMessage = "Ad en fazla 150 karakter olabilir.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    [StringLength(80, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "E-posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre tekrar alanı zorunludur.")]
    [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    // Opsiyonel alanlar
    [StringLength(50, ErrorMessage = "Ülke en fazla 50 karakter olabilir.")]
    public string Country { get; set; }

    [StringLength(50, ErrorMessage = "Şehir en fazla 50 karakter olabilir.")]
    public string City { get; set; }

    // Sistem tarafında gerekli alanlar
    public string ReturnUrl { get; set; }

    [Required(ErrorMessage = "Kullanıcı sözleşmesini kabul etmelisiniz.")]
    public bool AcceptTerms { get; set; }
}