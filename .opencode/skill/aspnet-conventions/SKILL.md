---
name: aspnet-conventions
description: Use ONLY when writing or reviewing ASP.NET Core code for the School Management System. Covers entity, controller, service, and EF Core conventions, naming, and layering rules from the project PRD.
---
# ASP.NET Coding Conventions

## Architecture
- Clean Architecture layering: `Domain` (entities, enums), `Application` (DTOs, service interfaces), `Infrastructure` (EF Core, external services), `Web` (controllers).
- Dependencies point inward only: Web → Application → Domain. Infrastructure implements Application interfaces.
- Multi-tenant: every entity that holds institution data has an `InstitutionId`; queries must always filter by the current tenant.

## Naming
- Entities: singular PascalCase (`Student`, `FeeInvoice`), interfaces prefixed with `I` (`IStudentService`).
- Controllers: `<Entity>Controller` with RESTful actions (`GetAll`, `GetById`, `Create`, `Update`, `Delete`).
- Async methods end with `Async`; use `CancellationToken` parameters.

## EF Core
- Configure entities with Fluent API in `Infrastructure/Persistence/<Entity>Configuration.cs`, not attributes.
- Soft delete via `IsDeleted` flag; never hard-delete tenant data.
- All queries `.AsNoTracking()` unless modifying.

## Controllers
- Thin controllers: no business logic — validate input, call a service, map to DTO, return `ActionResult<T>`.
- Return proper status codes: 201 for Create, 204 for Delete, 400 with ProblemDetails for validation errors.

## Before finishing any change
- Run `dotnet build` and fix all warnings before claiming success.
