using ADASOIdentityServer.Database.Models;

namespace ADASOIdentityServer.AuthServer.UI.Models.Users;

public class UsersIndexViewModel
{
    public IReadOnlyList<UserListRowViewModel> Users { get; set; } = [];
    public IReadOnlyList<UserType> UserTypes { get; set; } = [];
    public IReadOnlyList<UserTypeCountViewModel> TypeCounts { get; set; } = [];
    public string Search { get; set; } = string.Empty;
    public int? UserTypeId { get; set; }
    public bool Unassigned { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public int TotalCount { get; set; }
    public int FilteredCount { get; set; }
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(FilteredCount / (double)PageSize));
}

public class UserListRowViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Department { get; set; }
    public string? Title { get; set; }
    public string Role { get; set; } = string.Empty;
    public int? UserTypeId { get; set; }
    public string? UserTypeName { get; set; }
    public string? UserTypeCode { get; set; }
    public bool IsActive { get; set; }
}

public class UserTypeCountViewModel
{
    public int? UserTypeId { get; set; }
    public int Count { get; set; }
}
