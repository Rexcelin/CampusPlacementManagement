
using CampusPlacementManagement.Data;
using CampusPlacementManagement.Models;
using CampusPlacementManagement.Models.Enums;
using CampusPlacementManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CampusPlacementManagement.Repositories;

public class CollegeRepository : ICollegeRepository
{
    private readonly ApplicationDbContext _context;

    public CollegeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get college by ID
    public async Task<College?> GetByIdAsync(int collegeId)
    {
        return await _context.Colleges
            .FirstOrDefaultAsync(c => c.CollegeId == collegeId);
    }

    // Get college by unique code
    public async Task<College?> GetByCodeAsync(string collegeCode)
    {
        return await _context.Colleges
            .FirstOrDefaultAsync(c => c.CollegeCode == collegeCode);
    }

    // Get all colleges
    public async Task<List<College>> GetAllAsync()
    {
        return await _context.Colleges
            .AsNoTracking()
            .OrderBy(c => c.CollegeName)
            .ToListAsync();
    }

    // Get colleges by status
    public async Task<List<College>> GetByStatusAsync(
        CollegeStatus status)
    {
        return await _context.Colleges
            .AsNoTracking()
            .Where(c => c.Status == status)
            .OrderBy(c => c.CollegeName)
            .ToListAsync();
    }

    // Check whether college code already exists
    public async Task<bool> ExistsByCodeAsync(string collegeCode)
    {
        return await _context.Colleges
            .AnyAsync(c => c.CollegeCode == collegeCode);
    }

    // Add new college
    public async Task AddAsync(College college)
    {
        await _context.Colleges.AddAsync(college);
    }

    // Update existing college
    public Task UpdateAsync(College college)
    {
        _context.Colleges.Update(college);
        return Task.CompletedTask;
    }

    // Save database changes
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
