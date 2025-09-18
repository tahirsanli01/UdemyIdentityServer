namespace ADASOIdentityServer.AuthServer.UI.Models.UserProject
{
    public class UserProjectEditPostModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public List<int> SelectedRoleIds { get; set; } = new List<int>();
    }
}
