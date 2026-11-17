# Skill: testing

Use when writing, organizing, or running tests for the School Management System.

## Current state

- No test projects exist yet. When the first one is created, the conventions below apply.

## Project layout

- One xUnit test project per testable layer, named `<Project>.Tests`:
  - `SchoolManagementSystem.Application.Tests`
  - `SchoolManagementSystem.Infrastructure.Tests`
  - `SchoolManagementSystem.Web.Tests` (integration tests via `WebApplicationFactory`)
- Test projects live under `tests/` and are added to the solution (`dotnet sln add`).

## Conventions

- Framework: xUnit (matching .NET tooling defaults); assertions via xUnit's `Assert` — no extra assertion libraries unless already used.
- Test method naming: `MethodName_Scenario_ExpectedResult` (e.g. `CreateAsync_NewStudent_SetsAppliedStatus`).
- One behavior per test; arrange-act-assert ordering.
- Async service tests: await with `CancellationToken.None` (or default).

## Testing services against EF Core

- Use SQLite in-memory for realistic behavior (`KeepAlive` connection + `EnsureCreated`):

```csharp
var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connection)
    .Options;
```

- Seed `ITenantContext` with a fixed test `InstitutionId` and assert tenant isolation (a different tenant sees zero rows).
- Mock `ITenantContext` with a simple stub class; use NSubstitute or a hand-written stub — pick one and stay consistent.

## Running tests

```powershell
dotnet test
```

## Before finishing any change that includes tests

1. `dotnet build` — 0 warnings.
2. `dotnet test` — all green; report counts.
