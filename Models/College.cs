
using CampusPlacementManagement.Models.Enums;

namespace CampusPlacementManagement.Models;

public class College
{
    // Primary key
    public int CollegeId { get; set; }

    // College information
    public string CollegeName { get; set; } = string.Empty;

    public string CollegeCode { get; set; } = string.Empty;

    public string? UniversityName { get; set; }

    // Contact information
    public string OfficialEmail { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string? Website { get; set; }

    // Approval status
    public CollegeStatus Status { get; set; }
        = CollegeStatus.Pending;

    // Record creation date
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
