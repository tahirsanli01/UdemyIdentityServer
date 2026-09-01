using ADASOIdentityServer.Database.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADASOIdentityServer.AuthServer.UI.Models.Users;

public class UserProjectsModalViewModel
{
    public int UserId { get; set; }
    public string UserDisplayName { get; set; } = string.Empty;
    public IReadOnlyList<UserProjects> Assignments { get; set; } = [];
    public SelectList Projects { get; set; } = null!;
    public MultiSelectList Roles { get; set; } = null!;
}
