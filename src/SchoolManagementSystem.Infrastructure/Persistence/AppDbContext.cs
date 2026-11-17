using SchoolManagementSystem.Application.Tenancy;
using SchoolManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Student>()
            .HasQueryFilter(s => s.InstitutionId == _tenantContext.InstitutionId && !s.IsDeleted);
    }
}
