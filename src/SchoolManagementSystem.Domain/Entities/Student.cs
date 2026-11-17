using SchoolManagementSystem.Domain.Common;
using SchoolManagementSystem.Domain.Enums;

namespace SchoolManagementSystem.Domain.Entities;

public class Student : BaseEntity
{
    public string AdmissionNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? GuardianName { get; set; }

    public string? PreviousInstitution { get; set; }

    public string? ClassLevel { get; set; }

    public string? Section { get; set; }

    public string? RollNumber { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public StudentStatus Status { get; set; }
}
