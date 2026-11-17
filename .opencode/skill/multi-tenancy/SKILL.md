# Skill: multi-tenancy

Use when adding or modifying entities, queries, endpoints, or services that touch institution data in the School Management System.

## Core rules

- Every entity that holds institution data has a `Guid InstitutionId` (inherited via `BaseEntity` in `Domain/Common`).
- All tenant queries are isolated by a global EF Core query filter, set in `AppDbContext.OnModelCreating`:

```csharp
modelBuilder.Entity<Student>()
    .HasQueryFilter(s => s.InstitutionId == _tenantContext.InstitutionId && !s.IsDeleted);
```

- Never bypass the filter with `IgnoreQueryFilters()` unless there is an explicit, documented platform-level reason.
- New entities MUST get their own query filter in `AppDbContext.OnModelCreating` — filters do not inherit automatically.

## How the current tenant is resolved

- `ITenantContext.InstitutionId` (`Application/Tenancy`) is implemented by `Web/Tenancy/TenantContext.cs`.
- Resolution order: `institution_id` claim → `X-Institution-Id` request header → `Tenant:DefaultInstitutionId` from appsettings → `Guid.Empty`.
- When manually testing endpoints, send the `X-Institution-Id` header (default dev value: `00000000-0000-0000-0000-000000000001`).

## Setting InstitutionId on writes

- Services assign `InstitutionId = _tenantContext.InstitutionId` when creating entities (see `StudentService.CreateAsync`).
- Never take `InstitutionId` from client-supplied request bodies — it always comes from `ITenantContext`.

## Tenant-scoped uniqueness

- Uniqueness is scoped per tenant: composite unique indexes on `(InstitutionId, <BusinessKey>)`, e.g. `(InstitutionId, AdmissionNumber)` and `(InstitutionId, Email)` in `StudentConfiguration`.

## Checklist for a new tenant entity

1. Inherit from `BaseEntity`.
2. Add Fluent API config in `Infrastructure/Persistence/<Entity>Configuration.cs` (`InstitutionId` required, composite unique index if applicable).
3. Add a `DbSet<T>` in `AppDbContext`.
4. Add a global query filter (tenant + soft delete) in `AppDbContext.OnModelCreating`.
5. Set `InstitutionId` from `ITenantContext` in the service's create method.
6. Run `dotnet build` and fix all warnings.
