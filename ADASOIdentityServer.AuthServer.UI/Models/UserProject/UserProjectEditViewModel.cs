using ADASOIdentityServer.Database.Models;

namespace ADASOIdentityServer.AuthServer.UI.Models.UserProject
{
    public class UserProjectEditViewModel
    {
        public UserProjects UserProject { get; set; }

        // Multi-select için
        public List<int> SelectedRoleIds { get; set; } = new();
    }
}
