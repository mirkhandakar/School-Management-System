using SchoolManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolManagementSystem.Infrastructure.Persistence;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.InstitutionId).IsRequired();

        builder.Property(s => s.AdmissionNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Gender)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.PhoneNumber)
            .HasMaxLength(30);

        builder.Property(s => s.Address)
            .HasMaxLength(500);

        builder.Property(s => s.GuardianName)
            .HasMaxLength(150);

        builder.Property(s => s.PreviousInstitution)
            .HasMaxLength(200);

        builder.Property(s => s.ClassLevel)
            .HasMaxLength(50);

        builder.Property(s => s.Section)
            .HasMaxLength(20);

        builder.Property(s => s.RollNumber)
            .HasMaxLength(20);

        builder.HasIndex(s => new { s.InstitutionId, s.AdmissionNumber })
            .IsUnique();

        builder.HasIndex(s => new { s.InstitutionId, s.Email })
            .IsUnique();
    }
}
