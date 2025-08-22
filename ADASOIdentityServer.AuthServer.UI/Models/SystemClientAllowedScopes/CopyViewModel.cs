using System.ComponentModel.DataAnnotations;

public class CopyViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Lütfen yetkilerini kopyalayacağınız uygulamayı seçiniz.")]
    [Range(1, int.MaxValue, ErrorMessage = "Lütfen geçerli bir uygulama seçiniz.")]
    public int SourceSystemClientId { get; set; }

    [Required(ErrorMessage = "Lütfen kopyalama yapılacak uygulamayı seçiniz.")]
    [Range(1, int.MaxValue, ErrorMessage = "Lütfen geçerli bir uygulama seçiniz.")]
    [NotEqual("SourceSystemClientId", ErrorMessage = "Kaynak ve hedef uygulamalar aynı olamaz.")]
    public int TargetSystemClientId { get; set; }
}
