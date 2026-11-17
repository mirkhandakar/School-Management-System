using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Application.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }

    public Guid InstitutionId { get; set; }

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

    public string Status { get; set; } = string.Empty;
}

public class CreateStudentRequest
{
    [Required]
    [MaxLength(50)]
    public string AdmissionNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [MaxLength(20)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? PhoneNumber { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(150)]
    public string? GuardianName { get; set; }

    [MaxLength(200)]
    public string? PreviousInstitution { get; set; }

    [MaxLength(50)]
    public string? ClassLevel { get; set; }

    [MaxLength(20)]
    public string? Section { get; set; }

    [MaxLength(20)]
    public string? RollNumber { get; set; }

    [Required]
    public DateOnly EnrollmentDate { get; set; }
}

public class UpdateStudentRequest
{
    [Required]
    [MaxLength(50)]
    public string AdmissionNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [MaxLength(20)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? PhoneNumber { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(150)]
    public string? GuardianName { get; set; }

    [MaxLength(200)]
    public string? PreviousInstitution { get; set; }

    [MaxLength(50)]
    public string? ClassLevel { get; set; }

    [MaxLength(20)]
    public string? Section { get; set; }

    [MaxLength(20)]
    public string? RollNumber { get; set; }

    [Required]
    public DateOnly EnrollmentDate { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
}
