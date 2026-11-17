# Skill: ef-core-workflow

Use when creating or changing entities, DbContext, configurations, or running database commands in the School Management System.

## Current database state

- Provider: SQLite (`Microsoft.EntityFrameworkCore.Sqlite`), connection string `DefaultConnection` = `Data Source=school.db` (relative to `src/SchoolManagementSystem.Web` content root).
- Schema is created with `Database.EnsureCreatedAsync()` at app startup (in `Program.cs`) — there are **no migrations yet**.

## Switching to migrations

When the model is stable, replace `EnsureCreatedAsync()` in `Program.cs` with `Database.MigrateAsync()` and run:

```powershell
dotnet ef migrations add <PascalCaseDescription> --project src/SchoolManagementSystem.Infrastructure --startup-project src/SchoolManagementSystem.Web
dotnet ef database update --project src/SchoolManagementSystem.Infrastructure --startup-project src/SchoolManagementSystem.Web
```

- `dotnet ef` requires `Microsoft.EntityFrameworkCore.Design` in the startup project (`Web`) — add it when starting migrations.
- Never edit an already-applied migration; add a new one instead.
- After `EnsureCreated` has created a DB, changing the model requires deleting `school.db` (dev only) or switching to migrations.

## Model conventions

- Fluent API only — no mapping attributes on entities.
- Config files: `Infrastructure/Persistence/<Entity>Configuration.cs`, one per entity, implementing `IEntityTypeConfiguration<T>`; picked up automatically via `ApplyConfigurationsFromAssembly` in `AppDbContext`.
- Soft delete via `IsDeleted = true` on the entity (no hard deletes of tenant data); soft-deleted rows are hidden by the global query filter.
- Required string properties get `IsRequired()` + explicit `HasMaxLength` in configuration.
- Composite unique indexes per tenant: `(InstitutionId, <BusinessKey>)`.
- `AppDbContext` takes `ITenantContext` to build tenant query filters (see multi-tenancy skill).

## Verifying changes

1. `dotnet build` — must pass with 0 warnings.
2. For schema changes in dev: delete `school.db`, run the app, confirm startup creates the expected tables (startup logs the `CREATE TABLE` commands).
