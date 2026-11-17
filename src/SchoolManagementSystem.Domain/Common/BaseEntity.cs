namespace SchoolManagementSystem.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public Guid InstitutionId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }
}
