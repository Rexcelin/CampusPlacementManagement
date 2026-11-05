
using CampusPlacementManagement.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace CampusPlacementManagement.Models;

public class ApplicationUser : IdentityUser
{
    // User's full name
    public string FullName { get; set; } = string.Empty;

    // Current account status
    public AccountStatus AccountStatus { get; set; }
        = AccountStatus.Pending;

    // Account creation date
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Last updated date
    public DateTime? UpdatedAt { get; set; }
}
