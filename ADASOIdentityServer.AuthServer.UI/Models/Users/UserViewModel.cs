namespace ADASOIdentityServer.AuthServer.UI.Models.Users
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OId { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public string Projects { get; set; }
        public string Departments { get; set; }
        public string Title { get; set; } = "ADASO";


    }
}
