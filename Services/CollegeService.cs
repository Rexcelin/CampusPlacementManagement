using CampusPlacementManagement.DTOs.Colleges;
using CampusPlacementManagement.Models;
using CampusPlacementManagement.Models.Enums;
using CampusPlacementManagement.Repositories.Interfaces;
using CampusPlacementManagement.Services.Interfaces;

namespace CampusPlacementManagement.Services;

public class CollegeService : ICollegeService
{
    private readonly ICollegeRepository _repository;

    public CollegeService(ICollegeRepository repository)
    {
        _repository = repository;
    }

    // 1. Register a new college
    public async Task<CollegeResponseDto> CreateCollegeAsync(
        CreateCollegeDto dto)
    {
        bool exists = await _repository.ExistsByCodeAsync(dto.CollegeCode);

        if (exists)
            throw new InvalidOperationException(
                "A college with this College Code already exists.");

        var college = new College
        {
            CollegeName = dto.CollegeName,
            CollegeCode = dto.CollegeCode,
            UniversityName = dto.UniversityName,
            OfficialEmail = dto.OfficialEmail,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            Website = dto.Website,
            Status = CollegeStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(college);
        await _repository.SaveChangesAsync();

        return MapToResponseDto(college);
    }

    // 2. Get college by ID
    public async Task<CollegeResponseDto?> GetCollegeByIdAsync(
        int collegeId)
    {
        var college = await _repository.GetByIdAsync(collegeId);

        if (college == null)
            return null;

        return MapToResponseDto(college);
    }

    // 3. Get all colleges
    public async Task<List<CollegeResponseDto>> GetAllCollegesAsync()
    {
        var colleges = await _repository.GetAllAsync();

        return colleges.Select(MapToResponseDto).ToList();
    }

    // 4. Get colleges by status
    public async Task<List<CollegeResponseDto>> GetCollegesByStatusAsync(
        string status)
    {
        if (!Enum.TryParse<CollegeStatus>(
            status, true, out var collegeStatus))
        {
            throw new ArgumentException("Invalid college status.");
        }

        var colleges =
            await _repository.GetByStatusAsync(collegeStatus);

        return colleges.Select(MapToResponseDto).ToList();
    }

    // 5. Update college
    public async Task<CollegeResponseDto?> UpdateCollegeAsync(
        int collegeId,
        UpdateCollegeDto dto)
    {
        var college = await _repository.GetByIdAsync(collegeId);

        if (college == null)
            return null;

        var existingCollege =
            await _repository.GetByCodeAsync(dto.CollegeCode);

        if (existingCollege != null &&
            existingCollege.CollegeId != collegeId)
        {
            throw new InvalidOperationException(
                "Another college is already using this College Code.");
        }

        college.CollegeName = dto.CollegeName;
        college.CollegeCode = dto.CollegeCode;
        college.UniversityName = dto.UniversityName;
        college.OfficialEmail = dto.OfficialEmail;
        college.PhoneNumber = dto.PhoneNumber;
        college.Address = dto.Address;
        college.Website = dto.Website;

        await _repository.UpdateAsync(college);
        await _repository.SaveChangesAsync();

        return MapToResponseDto(college);
    }

    // 6. Approve college
    public async Task<CollegeResponseDto?> ApproveCollegeAsync(
        int collegeId)
    {
        var college = await _repository.GetByIdAsync(collegeId);

        if (college == null)
            return null;

        if (college.Status != CollegeStatus.Pending)
            throw new InvalidOperationException(
                "Only pending colleges can be approved.");

        college.Status = CollegeStatus.Approved;

        await _repository.UpdateAsync(college);
        await _repository.SaveChangesAsync();

        return MapToResponseDto(college);
    }

    // 7. Reject college
    public async Task<CollegeResponseDto?> RejectCollegeAsync(
        int collegeId)
    {
        var college = await _repository.GetByIdAsync(collegeId);

        if (college == null)
            return null;

        if (college.Status != CollegeStatus.Pending)
            throw new InvalidOperationException(
                "Only pending colleges can be rejected.");

        college.Status = CollegeStatus.Rejected;

        await _repository.UpdateAsync(college);
        await _repository.SaveChangesAsync();

        return MapToResponseDto(college);
    }

    // 8. Suspend college
    public async Task<CollegeResponseDto?> SuspendCollegeAsync(
        int collegeId)
    {
        var college = await _repository.GetByIdAsync(collegeId);

        if (college == null)
            return null;

        if (college.Status != CollegeStatus.Approved)
            throw new InvalidOperationException(
                "Only approved colleges can be suspended.");

        college.Status = CollegeStatus.Suspended;

        await _repository.UpdateAsync(college);
        await _repository.SaveChangesAsync();

        return MapToResponseDto(college);
    }

    // 9. Convert College model into response DTO
    private static CollegeResponseDto MapToResponseDto(
        College college)
    {
        return new CollegeResponseDto
        {
            CollegeId = college.CollegeId,
            CollegeName = college.CollegeName,
            CollegeCode = college.CollegeCode,
            UniversityName = college.UniversityName,
            OfficialEmail = college.OfficialEmail,
            PhoneNumber = college.PhoneNumber,
            Address = college.Address,
            Website = college.Website,
            Status = college.Status,
            CreatedAt = college.CreatedAt
        };
    }
}
