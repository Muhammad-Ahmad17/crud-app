# ClinicMvc — MVC CRUD (learn the architecture)

Server-rendered Patients CRUD so you can see **Model / View / Controller** clearly.

## Run

```bash
dotnet run --project week3/ClinicMvc
# open http://localhost:<port>/
# Patients: http://localhost:<port>/Patients
```

## Folder map = MVC

```
ClinicMvc/
├── Models/
│   └── Patient.cs              ← MODEL (data + [Required] validation)
├── Views/
│   ├── Home/Index.cshtml       ← architecture explainer page
│   └── Patients/
│       ├── Index.cshtml        ← list
│       ├── Details.cshtml
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       └── Delete.cshtml       ← VIEW (HTML)
├── Controllers/
│   └── PatientsController.cs   ← CONTROLLER (HTTP → Model → View)
├── Data/
│   ├── AppDbContext.cs         ← EF Core DB access
│   └── DbSeeder.cs
└── Program.cs                  ← DI + routing `{controller}/{action}/{id?}`
```

## One request walkthrough

`GET /Patients/Edit/1`

1. Route matches `PatientsController`, action `Edit`, `id=1`
2. Controller loads `Patient` from SQLite via `AppDbContext`
3. `return View(patient)` looks for `Views/Patients/Edit.cshtml`
4. Razor binds `@model Patient` and fills the form
5. User submits → `POST /Patients/Edit/1` → validation → `SaveChanges` → redirect to Index

## How this relates to ClinicApi

| ClinicMvc | ClinicApi |
|-----------|-----------|
| Returns HTML | Returns JSON |
| Controller → DbContext | Controller → Service → Repository |
| Good for learning MVC UI | Good for product APIs (CureMD-style) |

Same domain idea (patients); different delivery shape.
