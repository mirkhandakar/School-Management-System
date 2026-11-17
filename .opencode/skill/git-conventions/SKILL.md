# Skill: git-conventions

Use before committing changes to the School Management System repository.

## Commit messages

- Imperative mood, concise, no scope prefixes, no ticket numbers (repo style so far: `Initial commit`, `Added Markdown files`).
- One logical change per commit.
- Examples: `Added Student entity and controller`, `Added multi-tenant query filters`, `Added StudentService tests`.

## What to commit

- `src/` code, `sln`, `csproj`, `appsettings.json`.
- Docs and skills under `.opencode/skill/`.

## What to never commit

- `bin/`, `obj/`, build artifacts.
- `school.db`, `school.db-wal`, `school.db-shm` (runtime SQLite database).
- Secrets or connection strings with credentials. The default `Data Source=school.db` connection string is fine to commit.

## Safety rules

- Only commit when the user explicitly asks.
- Inspect `git status`, `git diff`, and `git log --oneline -10` first; stage only the intended files.
- If hooks reject a commit, fix the issue and create a new commit — do not amend the failed one.

## Housekeeping

- A `.gitignore` covering `.NET` (bin/obj) + `*.db` should exist before the first code commit — create it if missing.
