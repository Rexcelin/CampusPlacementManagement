
using CampusPlacementManagement.Models;

namespace CampusPlacementManagement.Repositories.Interfaces;

public interface ICollegeRepository
{
    Task<College?> GetByIdAsync(int collegeId);

    Task<College?> GetByCodeAsync(string collegeCode);

    Task<List<College>> GetAllAsync();

    Task<List<College>> GetByStatusAsync(
        Models.Enums.CollegeStatus status);

    Task<bool> ExistsByCodeAsync(string collegeCode);

    Task AddAsync(College college);

    Task UpdateAsync(College college);

    Task SaveChangesAsync();
}
