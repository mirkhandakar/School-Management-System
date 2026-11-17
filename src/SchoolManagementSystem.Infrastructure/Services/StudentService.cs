using SchoolManagementSystem.Application.DTOs;
using SchoolManagementSystem.Application.Services;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Domain.Enums;
using SchoolManagementSystem.Application.Tenancy;
using SchoolManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _dbContext;
    private readonly ITenantContext _tenantContext;

    public StudentService(AppDbContext dbContext, ITenantContext tenantContext)
    {
        _dbContext = dbContext;
        _tenantContext = tenantContext;
    }

    public async Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var students = await _dbContext.Students
            .AsNoTracking()
            .OrderBy(s => s.FirstName)
            .ThenBy(s => s.LastName)
            .ToListAsync(cancellationToken);

        return students.Select(MapToDto).ToList();
    }

    public async Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        return student is null ? null : MapToDto(student);
    }

    public async Task<StudentDto> CreateAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
    {
        var student = new Student
        {
            Id = Guid.NewGuid(),
            InstitutionId = _tenantContext.InstitutionId,
            AdmissionNumber = request.AdmissionNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            GuardianName = request.GuardianName,
            PreviousInstitution = request.PreviousInstitution,
            ClassLevel = request.ClassLevel,
            Section = request.Section,
            RollNumber = request.RollNumber,
            EnrollmentDate = request.EnrollmentDate,
            Status = StudentStatus.Applied,
            CreatedAtUtc = DateTime.UtcNow
        };

        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(student);
    }

    public async Task<StudentDto?> UpdateAsync(Guid id, UpdateStudentRequest request, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (student is null)
        {
            return null;
        }

        student.AdmissionNumber = request.AdmissionNumber;
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.DateOfBirth = request.DateOfBirth;
        student.Gender = request.Gender;
        student.Email = request.Email;
        student.PhoneNumber = request.PhoneNumber;
        student.Address = request.Address;
        student.GuardianName = request.GuardianName;
        student.PreviousInstitution = request.PreviousInstitution;
        student.ClassLevel = request.ClassLevel;
        student.Section = request.Section;
        student.RollNumber = request.RollNumber;
        student.EnrollmentDate = request.EnrollmentDate;
        student.Status = ParseStatus(request.Status);
        student.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(student);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (student is null)
        {
            return false;
        }

        student.IsDeleted = true;
        student.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static StudentStatus ParseStatus(string status)
    {
        return Enum.TryParse<StudentStatus>(status, ignoreCase: true, out var parsed)
            ? parsed
            : StudentStatus.Applied;
    }

    private static StudentDto MapToDto(Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            InstitutionId = student.InstitutionId,
            AdmissionNumber = student.AdmissionNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            Address = student.Address,
            GuardianName = student.GuardianName,
            PreviousInstitution = student.PreviousInstitution,
            ClassLevel = student.ClassLevel,
            Section = student.Section,
            RollNumber = student.RollNumber,
            EnrollmentDate = student.EnrollmentDate,
            Status = student.Status.ToString()
        };
    }
}
