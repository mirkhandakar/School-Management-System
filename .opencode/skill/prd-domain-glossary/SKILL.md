# Skill: prd-domain-glossary

Use when naming entities, fields, enums, or endpoints, or when implementing features — so code matches the PRD (`prd.md`, product name **EximusEdu**).

## Source of truth

- Full spec: `prd.md` (English, 10 sections including §10 Glossary).
- Bengali brochure mirror: `Markdownfiles from Clude/*.md` (00–07, part-based).

## Key domain terms

| PRD term | Code usage |
|---|---|
| Institution | Tenant; `InstitutionId` on every tenant entity |
| Student, Staff, Guardian | Singular PascalCase entities (`Student`, `Staff`, `Guardian`) |
| Admission | Online admission workflow `FR-HR-01` |
| Fee Invoice, Fee Receipt | Fees module entities (`FeeInvoice`, `FeeReceipt`) |
| Report Card | Academics module |
| Attendance, Leave | `FR-HR-04`, `FR-HR-05` |
| ID Card | `FR-HR-07` |

## Admission status pipeline (FR-HR-01)

`Applied → Fee Verified → Admit Card Issued → Admitted`

- Implemented as `StudentStatus` enum (`Domain/Enums/StudentStatus.cs`): `Applied`, `FeeVerified`, `AdmitCardIssued`, `Admitted`.
- New students are created with `Applied`; transitions happen via service logic, never client-supplied on create.

## Module map (§5 Functional Requirements)

- Module 1: Human Resources (Students & Staff) — admission, profiles, attendance, leave, ID cards
- Module 2: Academics — classes, sections, subjects, exams, results
- Module 3: Fees & Accounts — invoices, receipts, income
- Module 4: Auxiliary — library, transport, hostel, SMS
- Module 5: Student/Parent Panel (`FR-SP-01..10`) — profile, dues, attendance, results, notices
- Module 6: Teacher Panel
- Module 7: Medical College Add-ons
- Module 8: Cross-cutting — audit, i18n, RBAC

## Cross-cutting rules that affect naming/design

- **RBAC-first** (`FR-AU-08-01`): role-permission gate at the API layer; roles are institution-defined (Principal, Class Teacher, Accountant, Warden, etc.).
- **Bengali-first i18n** (§3.2): user-facing strings are localized resources, never hardcoded.
- **Audit everything**: user-level activity logs + audit trails on financial and academic mutations — plan for audit fields/services when adding mutating endpoints.
- **Event-driven automation**: attendance → payroll, fee receipt → SMS + income entry, absence → guardian SMS.

## Naming rule

When adding an entity or feature, first locate the matching PRD requirement ID (e.g. `FR-HR-03`) and use the PRD's terminology verbatim; if the PRD has no term, choose conventional EMS vocabulary and document it here.
