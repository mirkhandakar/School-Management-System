# EximusEdu — Education Management System (EMS)
## Product Requirements Document (PRD)

| Field | Value |
|---|---|
| **Product** | EximusEdu — Education Management System (SaaS) |
| **Version** | 1.0 (Draft) |
| **Date** | 2026-11-17 |
| **Source** | `EximusEdu_Brochure_v3.md` (converted from `EximusEdu_Brochure_v3.pdf`) |
| **Audience** | Engineering team, QA, UX, product stakeholders |
| **Status** | Draft — pending stakeholder review |

---

## 1. Product Overview

### 1.1 Vision

Run an entire educational institution — school, college, madrasa, university, or online education platform — from a single software. EximusEdu is a complete, modern Education Management System (EMS) that digitizes every academic and administrative workflow, from student admission to exam results, fee collection, payroll, and parent communication.

> "Just as a bank ATM card transformed into your financial management, we bring your entire institution onto your laptop or mobile — through one software."

### 1.2 Problem Statement

Institutions today manage attendance registers, mark ledgers, leave applications, report cards, and accounts manually — "the way of an earlier age." Teachers spend their days on administrative work instead of teaching. Guardians lack visibility into student progress. Institutions need an automated, secure, Bengali-first management system that eliminates manual communication and lets educators focus on education.

### 1.3 Product Goals

1. **Complete out-of-the-box:** meet 90%+ of an institution's needs with zero customization on day one.
2. **One platform, everything:** admission, profiles, attendance, exams, fees, accounts, and communication in a single system.
3. **Time & effort savings:** automate daily administrative operations (the brochure's core value proposition).
4. **Parent engagement:** give guardians real-time online access to results, attendance, notices, and fees.
5. **Trust at scale:** proven across 200+ institutions, including those with 25,000–35,000+ students and 125+ campuses.

### 1.4 Success Metrics

| Metric | Target |
|---|---|
| Feature coverage without customization | ≥ 90% of a typical institution's requirements |
| Configurable features | 150+ features across modules |
| Adoption | 200+ institutions onboarded |
| Teacher time reclaimed | Reduction in daily admin hours (baseline vs. post-adoption survey) |
| Attendance improvement | Measurable attendance lift after absence-SMS enablement (case-study evidence: 30% rise in subject-wise attendance within 3 months at a medical college) |
| Support response | 24/7 support; real-time call support for critical issues |
| Security | Zero data-breach incidents; successful VAPT (Vulnerability Assessment & Penetration Testing) sign-off |

---

## 2. Users & Personas

### 2.1 Personas

| Persona | Description | Primary panels |
|---|---|---|
| **Super Admin (SaaS Operator)** | EximusEdu staff who onboard institutions, manage subscriptions, configure tenant settings | Tenancy console |
| **Admin / Principal (Institution Head)** | Manages the full institution: admissions, approvals, fees, results publication, notices, committees | Admin web panel |
| **Teacher / Hifz Supervisor** | Takes attendance, enters marks, publishes lesson plans/notes, evaluates lessons | Web + mobile app |
| **Student** | Views own results, attendance, fees, notices; submits assignments & leave applications | Web + mobile app |
| **Parent / Guardian** | Pays fees online, monitors attendance, results, notices; messages teachers | Mobile app (primary), web |
| **Accountant / Staff** | Fee collection at cash counter, income-expense entries, vouchers, payroll support | Admin web panel |
| **Librarian / Inventory Officer** | Manages library lending and warehouse stock | Admin web panel |
| **Hostel Warden** | Room setup, seat allocation, monthly dues | Admin web panel |
| **Donor / Foundation** | Receives donation receipts and thank-you/reminders | SMS + receipts |

### 2.2 Role & Permission Model

- **RBAC-first:** every feature and action is gated by role-permission configuration (`FR-AU-08-01`). Roles are institution-defined (e.g., Principal, Vice-Principal, Class Teacher, Subject Teacher, Accountant, Warden, Exam Controller).
- Every role-feature-action combination is auditable; all activity is captured in activity logs and audit trails.
- Default personas above are **seeded roles**; institutions may add custom roles at any time.

---

## 3. Platform & UX Strategy

### 3.1 Platforms

| Platform | Purpose | Notes |
|---|---|---|
| **Responsive Web (Admin + Panels)** | Full-feature dashboards for admin, teacher, student, parent | Works on desktop, tablet, mobile browsers |
| **Mobile App (iOS/Android)** | Student, parent, and teacher day-to-day actions | Login-based push notifications; **all core features must be operable from the mobile app** (brochure requirement) |
| **Attendance Devices** | RFID/ID-card readers, fingerprint, face recognition | Auto-syncs attendance into the system |

### 3.2 Localization (i18n)

- **Bengali-first UI** with English toggle; all user-facing strings localized via i18n resources.
- Bengali numeral support in reports where institution-preferred; Latin numerals option.
- In-app chat and SMS content support templates with Bengali text.

### 3.3 UI/UX Design Principles

1. **Dashboard-first, one-glance views.** Every panel opens with a dashboard; system-wide views ("one glance at the whole institution's attendance/results") are first-class screens.
2. **Zero-training design.** Simple, intuitive interface — no lengthy tutorials (brochure promise: "no complexity or long tutorials").
3. **Bengali-first typography.** Full support for Bengali complex-script rendering; dedicated display font pairing; numerals and dates localized (Bengali calendar awareness).
4. **Progressive disclosure.** Admin screens with 8+ inputs (report card config, exam setup) use stepped wizards with live preview.
5. **Document previews, not just downloads.** Admit cards, report cards, payslips, receipts render as on-screen cards before print/download.
6. **Mobile parity.** Any action available on the teacher/student/parent panels must be reachable in ≤ 3 taps on mobile.
7. **Trust & transparency.** Security posture (VAPT-tested, audit trails) is visible to admins; user data is treated as fully confidential.
8. **Accessibility.** WCAG 2.1 AA target: contrast, keyboard navigation, screen-reader labels on both languages.
9. **Low-bandwidth resilience.** Lightweight pages, paginated tables, optimistic UI; usable on rural connections.

---

## 4. System Architecture (SaaS, Multi-Tenant)

### 4.1 Tenancy

- **Multi-tenant SaaS:** one platform, many institutions. Tenant data is logically isolated (tenant-scoped schemas/DB-per-tenant option for large institutions).
- Each institution gets a unique tenant workspace; institution **websites host public online admission/recruitment forms** that flow into the tenant's admin panel (`FR-HR-01`).
- Supports institutions with **multiple campuses/branches** and **125+ campuses under one tenant** (per brochure client profile).

### 4.2 Technology Stack

| Layer | Technology | Rationale |
|---|---|---|
| Backend | **ASP.NET Core (C#)** modular monolith, microservice-ready module boundaries | Robust enterprise ecosystem, long-term support, team alignment |
| Frontend (web) | **React** (or Angular) SPA with design system | Rich dashboards, component reuse across panels |
| Mobile | Cross-platform app (React Native or Flutter) sharing API with web | Single codebase for student/parent/teacher apps |
| Database | PostgreSQL / SQL Server (tenant-isolated) | Relational integrity for academic/finance data |
| Messaging | Queue-based workers for SMS, notifications, scheduled reports | Reliable bulk SMS + automatic notifications |
| Storage | Object storage for photos, documents, ID-card designs | Reports, scanned admission forms |

### 4.3 Cross-Cutting Architecture Rules

- **RBAC enforcement at API layer** — every endpoint checks role-permission; UI hides what API denies.
- **Audit everything:** user-level activity logs + audit trails on financial and academic mutations.
- **Event-driven automation:** attendance → payroll and report-card feeds; fee receipt → SMS + income entry; absence detection → guardian SMS.
- **VAPT-tested security posture:** regular vulnerability assessment and penetration testing; encryption in transit and at rest; **user data is never sold, leaked, or misused** (hard product principle).
- **Customization hook layer:** institutions' unique workflows are accommodated via configurable workflows and custom fields without core forks (brochure promise of per-institution customization).

---

## 5. Functional Requirements

> Each requirement lists the source brochure capability, key user stories, and acceptance criteria. IDs are stable references for backlog/task mapping.

### Module 1 — Human Resource Management (Students & Staff)

#### FR-HR-01: Online Student Admission
**Source:** Brochure p. 11 — "শিক্ষার্থী অনলাইন ভর্তি"

**User stories**
- As a guardian, I want to apply online from the institution's website and pay the application fee, so that I don't visit the campus.
- As an admin, I want to see the list of online applications with their full admission forms, so I can process them.

**Acceptance criteria**
1. Institution website exposes an admission form (public, tenant-branded); applicant can pay the application fee online.
2. After fee verification, the applicant receives an **admit card for the admission test**.
3. Completed application forms are viewable and downloadable by admins.
4. Admin sees application status pipeline: Applied → Fee Verified → Admit Card Issued → (Result) → Admitted.

#### FR-HR-02: Online Teacher/Staff Recruitment
**Source:** Brochure p. 12 — "শিক্ষক/স্টাফ অনলাইন নিয়োগ"

**Acceptance criteria**
1. Public recruitment form on institution website with online fee payment.
2. Fee verification issues an **exam admit card** for the recruitment test.
3. Admin views applications list + downloadable full application documents.

#### FR-HR-03: Student & Staff Profiles
**Source:** Brochure p. 13 — "শিক্ষার্থী ও শিক্ষক প্রোফাইল"

**Acceptance criteria**
1. Student, teacher, and staff profiles with institution-defined custom fields.
2. Searchable/filterable directory lists (class, section, subject, department).
3. Profile export/download for student and teacher records.

#### FR-HR-04: Integrated Attendance System
**Source:** Brochure p. 14 — "ইন্টিগ্রেটেড হাজিরা" (with reports & notifications)

**User stories**
- As a teacher, I want to take class attendance from my mobile app, so that I don't carry a register.
- As an admin, I want automatic attendance via ID card/fingerprint/face devices, so that records are tamper-proof.
- As a parent, I want an SMS when my child is absent, so that I know immediately.

**Acceptance criteria**
1. Teachers mark attendance **from the mobile app** (manual roll-call).
2. Attendance devices (ID card/RFID, fingerprint, face recognition) auto-record attendance into the system.
3. Detailed attendance reports for students, teachers, staff, officers.
4. Automatic **SMS/notifications**: student present / absent / late to guardians.
5. Exam report cards auto-populate attendance data.
6. Staff attendance auto-flows into **payroll sheets** with institution-defined rules and adjustable allowances.
7. Institution-wide "one-glance" attendance dashboards (all students; per section; staff).

#### FR-HR-05: Leave Management (Students & Staff)
**Source:** Brochure p. 18

**Acceptance criteria**
1. Students and staff can submit leave applications online.
2. Multi-step approval/rejection with comment trail (recommendation → forwarding → final approval).
3. Leave counts and detailed reports per person.

#### FR-HR-06: Certificates
**Source:** Brochure p. 20 — character certificates, transfer certificates, etc.

**Acceptance criteria**
1. Institution issues predefined certificate types (character, transfer, others).
2. Templates are customizable; issued certificates are logged and printable.

#### FR-HR-07: Dynamic ID Cards
**Source:** Brochure p. 21 — "আইডি কার্ড"

**Acceptance criteria**
1. Dynamic template designer: place data fields anywhere on the card.
2. Different designs per institution, per campus, per class/shift.
3. Separate staff/designation card designs.
4. Customizable **back side** design per institution requirements.
5. Print-ready output for any paper/format (cards sheets).

---

### Module 2 — Academics

#### FR-AC-01: Exam Framework & Custom Report Cards
**Source:** Brochure pp. 23–24 — "সম্পূর্ণ কাস্টমাইজড রিপোর্ট কার্ড"

**User stories**
- As an exam controller, I want class tests, half-yearly exams, homework, and continuous assessments to each contribute a configured percentage to the semester total, so that evaluation matches our curriculum.
- As a teacher, I want to enter special evaluations and remarks so that report cards reflect more than marks.

**Acceptance criteria**
1. Multiple exam types (class test, half-yearly, homework, continuous assessment, …) each map to a configurable weight of the semester final.
2. Per-subject aggregation: marks/GPA display and pass/fail determination per subject.
3. Optional/additional subjects appear **separately** and do not affect the main result.
4. Automatic grade entry; no manual grade cross-checking needed.
5. Auto merit positions at **branch and section level** based on GPA/total marks.
6. Teacher special evaluations and remarks entry.
7. Attendance auto-populates on the report card.
8. Fully customizable card layouts per institution.

#### FR-AC-02: Tabulation Sheets
**Source:** Brochure pp. 27–30

**Acceptance criteria**
1. **Short view:** subject-wise best-five, average marks, pass counts, golden-A counts; per-student total, GPA, grade sheet, section/class merit, golden-A subject count.
2. **Detailed view:** deeper per-subject marks data and extended statistics.

#### FR-AC-03: Online Result Publication
**Source:** Brochure p. 32

**Acceptance criteria**
1. Admin publishes results online; students/parents view them in their panels.
2. Publication is controlled per exam with visibility toggle.

#### FR-AC-04: Merit Lists & Comparative Results
**Source:** Brochure p. 34

**Acceptance criteria**
1. Merit list generation.
2. Section-wise and institution-wide one-glance result comparisons.

#### FR-AC-05: Admit Cards
**Source:** Brochure p. 36

**Acceptance criteria**
1. Subject & subject-code display at a glance.
2. Auto-attached principal's signature area.
3. Contact block (phone, email, website link) per institution.
4. Student photo, and required fields (name, roll, section).
5. Exam date & time per subject; per-day remarks/warnings field.
6. Batch print/download.

#### FR-AC-06: Seat Plans (আসন বিন্যাস)
**Source:** Brochure p. 36

**Acceptance criteria**
1. Auto seat allocation with student photos and details (name, roll, section, shift).
2. Editable and printable seat plan sheets.

#### FR-AC-07: Class & Teacher Routines
**Source:** Brochure pp. 38–40

**Acceptance criteria**
1. Class routine per branch/section.
2. Consolidated routine for all classes (students).
3. Per-teacher routine; teachers' one-glance weekly and daily routine views.

#### FR-AC-08: Lesson Plans & Evaluation
**Source:** Brochure p. 42 — "পাঠ পরিকল্পনা ও মূল্যায়ন"

**Acceptance criteria**
1. Teachers create lesson plans; daily diary per day.
2. Students see daily lessons, notes, and subject-wise **monthly progress reports**.

#### FR-AC-09: Course Management (Online Class)
**Source:** Brochure p. 44 — "কোর্স ব্যবস্থাপনা (গুগল ক্লাসরুম অনুরূপ)"

**Acceptance criteria**
1. Course activity management per subject/class.
2. Students submit assignments online; teachers view and mark submissions online (paperless).

#### FR-AC-10: Online MCQ Quizzes, Class Notes, Syllabus
**Source:** Brochure p. 46

**Acceptance criteria**
1. Online MCQ quiz creation, taking, and auto-grading.
2. Class notes distribution per course/subject.
3. Syllabus publication and tracking.

---

### Module 3 — Fees & Accounts

#### FR-FA-01: Fee Collection (Cash + Online)
**Source:** Brochure p. 48

**Acceptance criteria**
1. Institution collects fees at a **cash counter** (choose counter, collect fee).
2. Guardians pay fees **online from home** via the parent panel.
3. Receipt print (রিশিদ প্রিন্ট) immediately after successful collection.
4. Automatic SMS notification on fee payment.

#### FR-FA-02: Fee Accounting Integration
**Source:** Brochure p. 48

**Acceptance criteria**
1. Each fee collection auto-creates the **income entry** in Accounts (no double entry).
2. Expense-side entries appear in Accounts statements with proper reflection.

#### FR-FA-03: Collection & Due Reports
**Source:** Brochure p. 50

**Acceptance criteria**
1. **Class-wise and receipt-wise** fee collection reports.
2. Due (বকেয়া) reports viewable; **due SMS** sent to guardians (auto or manual).
3. Waiver/discount report support (brochure feature list).

#### FR-FA-04: Income–Expense Accounting
**Source:** Brochure pp. 52–56

**Acceptance criteria**
1. Daily income–expense **voucher top-sheet** (ম্যানুয়াল ভাউচার টপ-শীট style).
2. **Full income–expense statement** (আয়-ব্যয় স্টেটমেন্ট) with chart of accounts.
3. **Ledger book (খতিয়ান)** in short and detailed views; printable.

---

### Module 4 — Auxiliary Modules

#### FR-AU-01: Inventory
**Source:** Brochure p. 58

**Acceptance criteria**
1. Item list with warehouse/godown-wise stock quantities.
2. Purchase records from suppliers (cash/bank) → **auto stock increment**.
3. Sales to students/teachers/staff with receipt print → **auto stock decrement**.
4. Item transfer between warehouses with records.

#### FR-AU-02: Library
**Source:** Brochure p. 60

**Acceptance criteria**
1. Catalog of books/CDs/magazines with search.
2. Item detail + copy counts.
3. Lending with due dates and return tracking.

#### FR-AU-03: Hostel
**Source:** Brochure p. 60

**Acceptance criteria**
1. Setup: halls, floors, rooms.
2. Room allocation to students with list + search; allocation history/log.
3. Automatic monthly dues for room/meals per allocation; dues collectible via fee flow.

#### FR-AU-04: Payroll (পে-রোল)
**Source:** Brochure p. 62

**Acceptance criteria**
1. Monthly salary sheets auto-generated.
2. Attendance-based salary calculation with special-case adjustments/exemptions.
3. Payslip download per employee.
4. **Provident fund** ledger with any-time reports.

#### FR-AU-05: Donation
**Source:** Brochure p. 62

**Acceptance criteria**
1. Donation collection with receipts; automatic thank-you SMS.
2. Donor registry (data retention).
3. Monthly/annual pledge-based automatic dues; reminder SMS before collection.

#### FR-AU-06: Messaging & SMS Engine
**Source:** Brochure p. 64

**Acceptance criteria**
1. In-app chat between guardians and teachers with login-based push notifications.
2. SMS sending: template-based with variable fields (personalization), masking/non-masking modes.
3. Auto-SMS categories: dues, fees, results, attendance, etc., plus manual sending; templates saveable.
4. SMS **groups** by students/teachers or raw mobile numbers; group templates with preview.
5. Sent-SMS history report.

#### FR-AU-07: Hifz (হিফয়)
**Source:** Brochure p. 66

**Acceptance criteria**
1. Hifz departments, teams, team-teachers management.
2. Department/team-wise student lists with filter/search.
3. Monthly progress reports.
4. Separate exams, results, tabulation sheets, and merit lists for Hifz.

#### FR-AU-08: Administrative Management
**Source:** Brochure p. 68

**Acceptance criteria**
1. **Role-permission management** for teachers & admins (access levels per feature).
2. Password management: individual reset via SMS; bulk default-reset with SMS or printable distribution.
3. Committee formation and management.
4. Guardian feedback/suggestion/complaint inbox with replies.

---

### Module 5 — Student / Parent Panel

**Source:** Brochure pp. 69–74

| ID | Feature | Acceptance criteria |
|---|---|---|
| FR-SP-01 | Dashboard | Personalized overview: notices, dues, attendance summary, recent results |
| FR-SP-02 | Profile | View own profile; guardian sees child profile(s) |
| FR-SP-03 | Fees | Due list, **online payment**, receipt download |
| FR-SP-04 | Attendance | Own attendance history; absence notifications |
| FR-SP-05 | Exams | Exam routine; marks; published results & report cards |
| FR-SP-06 | Notices & Events | Institution notices and event list |
| FR-SP-07 | Academics | Daily lesson plan, teacher's diary, daily lesson evaluation, class notes |
| FR-SP-08 | Assignments & Quizzes | Submit assignments online (course module); take MCQ quizzes |
| FR-SP-09 | Messaging | Chat with teachers; push notifications in-app |
| FR-SP-10 | Leave | Submit leave applications; track approval status |

### Module 6 — Teacher Panel

**Source:** Brochure pp. 75–78

| ID | Feature | Acceptance criteria |
|---|---|---|
| FR-TP-01 | Dashboard | Classes, routines, tasks at a glance |
| FR-TP-02 | Student directory | View class/section student lists and profiles |
| FR-TP-03 | Attendance | Take class attendance (mobile app); view/edit history within rules |
| FR-TP-04 | Marks entry | Enter exam marks and special evaluations |
| FR-TP-05 | Lesson evaluation | Enter daily lesson/learning evaluation for students |
| FR-TP-06 | Lesson plans & notes | Publish lesson plans, class notes, syllabus updates |
| FR-TP-07 | Leave | Submit own leave applications; view student leave requests |
| FR-TP-08 | Payroll | View own payslip and provident fund statement |
| FR-TP-09 | P-P (Peer) features | View payslip/PF-related fund documents (per brochure "পে-রোল ও প্রোভিডেন্ট ফান্ড দেখা") |

### Module 7 — Medical College Add-ons

**Source:** Brochure pp. 79–84

| ID | Feature | Acceptance criteria |
|---|---|---|
| FR-MC-01 | Batch/Ward attendance | Attendance per lecture, tutorial, ward — full batch or ward-group basis |
| FR-MC-02 | Detailed attendance reports | Date-wise detailed presence reports on demand |
| FR-MC-03 | Absence escalation | SMS to student/guardian on absence; **in-app alert when any subject's attendance < 75%** |
| FR-MC-04 | Student visibility | Student sees detailed attendance; subjects below 75% flagged ("mark") in one glance |
| FR-MC-05 | Leave workflow | Student leave with step-wise recommendation/forwarding → final approve/reject; leave counts & reports |
| FR-MC-06 | Item–Card–Term exams | Card exam number sheets with auto merit & fail highlighting; term exam entry auto-generates merit + fail highlights |
| FR-MC-07 | Prof exam eligibility | **1-click** eligibility report; non-eligible reasons (attendance per subject / exam marks) shown |
| FR-MC-08 | Teacher evaluation | Students evaluate teachers anonymously; per-teacher summary reports |

> **Case study (target metric):** After enabling absence SMS at Faridpur Medical College, attendance rose noticeably from day one; 30% subject-wise attendance increase within 3 months of adoption (brochure, p. 80).

### Module 8 — Cross-Cutting Capabilities

| ID | Feature | Acceptance criteria |
|---|---|---|
| FR-XC-01 | Notification center | Push + in-app notifications for messages, notices, fees, attendance, results |
| FR-XC-02 | SMS automation | Event-triggered SMS flows (attendance, dues, results, fees) + manual campaigns |
| FR-XC-03 | Activity log & audit trail | All mutations logged per user with before/after where relevant |
| FR-XC-04 | Reports library | 150+ features/smart reports spanning modules (brochure claim); report samples per appendix |
| FR-XC-05 | AI analytics (Roadmap) | Auto-generated per-student analytical reports (results, homework, attendance, teacher feedback) identifying at-risk and high-potential students (brochure: in development) |

---

## 6. External Integrations

| Integration | Details |
|---|---|
| **SMS Gateway(s)** | Masking & non-masking; delivery reports; template + variable support; bulk + scheduled sending |
| **Payment Gateway(s)** | Online fee/application-fee/donation payments (local BD rails: bKash/Nagad/cards/banks via PSP) |
| **Attendance Devices** | RFID/ID-card readers, fingerprint biometrics, face recognition — sync into FR-HR-04 |
| **Institution Website** | Public admission/recruitment forms embedded in tenant-branded sites |

---

## 7. Non-Functional Requirements

| Category | Requirement |
|---|---|
| **Performance** | Core pages ≤ 2s load on 3G-class connections; attendance marking for 60-student class ≤ 3s round-trip; report generation ≤ 5s for class-sized data sets |
| **Scalability** | Support 200+ tenants; single tenant with 35,000+ students and 125+ campuses (largest brochure client profile); elastic scaling for admission-season spikes |
| **Availability** | ≥ 99.9% monthly uptime; rolling maintenance outside peak hours |
| **Security** | VAPT-tested (annual + on major releases); OWASP ASVS alignment; encryption in transit (TLS 1.2+) and at rest; strict data confidentiality — **no user data sale/leak/misuse, ever** |
| **Audit & compliance** | Immutable audit trails for academic + financial actions; configurable data retention |
| **Backup & DR** | Daily backups; documented RTO/RPO; tenant restore capability |
| **i18n** | Full BN/EN coverage including dates, numerals, SMS templates; complex-script rendering |
| **Browser/device support** | Latest 2 versions of major browsers; Android 8+ / iOS 14+ for apps |
| **Supportability** | 24/7 support channel; real-time call support for critical issues; in-app support entry point |
| **Observability** | Structured logging, error tracking, per-tenant usage metrics |

---

## 8. Roadmap & Phasing

### Phase 1 — MVP (Core Operations)
- Tenancy + RBAC + admin panel foundation
- FR-HR-01..04 (admission, recruitment, profiles, attendance)
- FR-AC-01..04 (exams, report cards, tabulation, results)
- FR-FA-01..03 (fee collection, accounting feed, reports)
- Student/Parent + Teacher panels (read + core actions)
- SMS engine (core events), notifications

### Phase 2 — Full Suite
- FR-HR-05..07 (leave, certificates, ID cards)
- FR-AC-05..10 (admit cards, seat plans, routines, lesson plans, courses, quizzes)
- FR-FA-04 (ledger/statements full)
- FR-AU-01..08 (all auxiliary modules)
- Attendance device integrations
- Medical college add-ons (FR-MC-*)

### Phase 3 — Intelligence & Scale
- FR-XC-05 AI analytics (per-student analytical reports, early-warning for at-risk students)
- Advanced per-institution workflow customization
- Certificate/ID design marketplace, expanded integrations

---

## 9. Assumptions, Risks & Out of Scope

### Assumptions
1. Institutions supply their own SMS gateway accounts or use the platform's pooled gateway (commercial model TBD).
2. Payment gateway onboarding is per-tenant (or platform-managed PSP, per commercial decision).
3. Bengali-first content; English translations provided by the product team.
4. Brochure feature list (150+ features) is treated as the upper-bound scope; this PRD maps the featured subset.

### Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Bengali complex-script rendering inconsistencies across devices | UI defects | Font strategy + automated visual tests on target devices |
| SMS gateway variability (BD operators) | Notification reliability | Multi-gateway abstraction, queue retries, delivery reports |
| Exam-config complexity (weights, optional subjects, merits) | Incorrect results | Config validation, preview-before-publish, dry-run mode |
| Multi-campus data scale (125+ campuses) | Performance | Tenant/campus partitioning, paginated reports, async generation |
| Attendance device firmware variety | Integration defects | Vendor adapters + integration test harness |
| Parent adoption of online payment | Low feature utilization | Guardian onboarding SMS guides, simple 3-step payment UX |

### Out of Scope (v1)
- Native desktop applications
- Hardware manufacturing/supply of attendance devices (integration only)
- Accounting standard compliance modules beyond income–expense statements/ledger (e.g., statutory tax filing)
- Content delivery network for third-party course content (beyond course management module)

---

## 10. Glossary

| Term | Meaning |
|---|---|
| **EMS** | Education Management System |
| **Tenant** | One institution (school/college/madrasa/university) using the SaaS |
| **Batch / Ward** | Medical-college student cohort groups used in FR-MC-01 |
| **Item–Card–Term exam** | Medical college exam ladder: item → card → term (professional) exams |
| **Prof exam** | Professional (final) exam in medical colleges; eligibility governed by attendance & marks |
| **Golden A (গোল্ডেন এ+)** | Result grade distinction (all A+ subjects) in BD grading |
| **Tabulation sheet** | Consolidated exam result sheet (short/detailed views) |
| **Ledger book (খতিয়ান)** | Double-entry account register view |
| **Due (বকেয়া)** | Outstanding fee balance |
| **Masking / Non-masking SMS** | SMS sent with branded sender ID vs. generic number |
| **Hifz** | Quran memorization program module |
| **VAPT** | Vulnerability Assessment and Penetration Testing |
| **RBAC** | Role-Based Access Control |

---

*End of PRD — EximusEdu v1.0. Derived from `EximusEdu_Brochure_v3.md`; brochure page references map to the converted markdown source.*
