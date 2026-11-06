
using CampusPlacementManagement.DTOs.Colleges;

namespace CampusPlacementManagement.Services.Interfaces;

public interface ICollegeService
{
    Task<CollegeResponseDto> CreateCollegeAsync(CreateCollegeDto dto);

    Task<CollegeResponseDto?> GetCollegeByIdAsync(int collegeId);

    Task<List<CollegeResponseDto>> GetAllCollegesAsync();

    Task<List<CollegeResponseDto>> GetCollegesByStatusAsync(string status);

    Task<CollegeResponseDto?> UpdateCollegeAsync(
        int collegeId,
        UpdateCollegeDto dto);

    Task<CollegeResponseDto?> ApproveCollegeAsync(int collegeId);

    Task<CollegeResponseDto?> RejectCollegeAsync(int collegeId);

    Task<CollegeResponseDto?> SuspendCollegeAsync(int collegeId);
}
