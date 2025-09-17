using System.ComponentModel.DataAnnotations.Schema;

namespace ADASOIdentityServer.Database.Partial
{
    public class UserProjectDto
    {
        [NotMapped]
        public List<int> SelectedRoleIds { get; set; }
    }
}
