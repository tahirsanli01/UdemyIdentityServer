namespace ADASOIdentityServer.AuthServer.Models
{
    public class EmailDto
    {
        public string Email { get; set; }
        public string? StaffEmail { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}