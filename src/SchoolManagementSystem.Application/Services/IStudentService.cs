using SchoolManagementSystem.Application.DTOs;

namespace SchoolManagementSystem.Application.Services;

public interface IStudentService
{
    Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<StudentDto> CreateAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);

    Task<StudentDto?> UpdateAsync(Guid id, UpdateStudentRequest request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
