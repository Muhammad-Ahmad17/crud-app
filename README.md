# 1-Month .NET Job Prep — CureMD / Systems Ltd / NetSol

Final-year prep workspace: C# OOP → SQL/ADO.NET/EF → portfolio Clinic API → interview drills.

## Quick start

```bash
# Prerequisites: .NET SDK 10+ (dotnet --version), VS Code + C# Dev Kit
dotnet --version

# Week 1 OOP demos
dotnet run --project week1/BankAccount
dotnet run --project week1/LibrarySystem
dotnet run --project week1/StudentGrades
dotnet run --project week1/CodingDrill

# Week 2
dotnet run --project week2/AdoNetDemo
dotnet run --project week2/EfCoreDemo
dotnet run --project week2/WebApiDemo
dotnet run --project week2/CodingDrill2

# Week 3 portfolio API
dotnet run --project week3/ClinicApi
# → http://localhost:<port>/swagger
dotnet test week3/ClinicApi.Tests

# Week 3 coding drill
dotnet run --project week3/CodingDrill3
```

## Folder map

| Path | Purpose |
|------|---------|
| [`week1/`](week1/) | C# + OOP console apps + problems 1–15 + CCAT start |
| [`week2/`](week2/) | SQL drills, ADO.NET, EF Core, ASP.NET Core (Express map) + problems 16–30 |
| [`week3/ClinicApi/`](week3/ClinicApi/) | **Portfolio** healthcare CRUD (EF + ADO.NET report) |
| [`week4/`](week4/) | Interview Q&A, CV template, application tracker, mock checklist |
| [`progress/`](progress/) | Daily 90-min ritual + progress log |

## Non-negotiable rule

**90 minutes every day writing code from a blank file with no AI.**  
See [`progress/DAILY_RITUAL.md`](progress/DAILY_RITUAL.md).

## Portfolio highlight

`week3/ClinicApi` — layered ASP.NET Core API:

- Controllers → Services → Repositories (DI)
- EF Core CRUD for doctors / patients / appointments
- **ADO.NET** JOIN report at `GET /api/appointments/report`
- Pagination, validation, exception middleware, Swagger, 10 xUnit tests

## Push to GitHub

Repo (already pushed): **https://github.com/Muhammad-Ahmad17/crud-app**

Put that URL on your CV (`week4/CV_TEMPLATE.md`).

## Honest expectations

One month gets you **fresh-grad interview-ready**, not senior. The filter is timed aptitude + independent coding + OOP/SQL fundamentals.
