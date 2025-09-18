using ADASOIdentityServer.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ADASOIdentityServer.AuthServer.Models
{
    public class CustomUser
    {
        public int Id { get; set; }
        public string OId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; }
        public DateTime? EmailConfirmationExpiry { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public List<Projects> Projects { get; set; }
        public List<UserProjectRole>? UserProjectRoles { get; set; } = new List<UserProjectRole>();
    }
}