namespace ADASOIdentityServer.Database.Models;

public static class UserProjectRoleModel
{
    public static ProjectRole projectRole { get; set; }
}

public enum ProjectRole
{
    ReadOnly = 1,
    ReadWrite = 2,
    Admin = 3
}