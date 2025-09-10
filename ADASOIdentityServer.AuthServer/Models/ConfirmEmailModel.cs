namespace ADASOIdentityServer.AuthServer.Models
{
    public class ConfirmEmailModel
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string CallbackUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
