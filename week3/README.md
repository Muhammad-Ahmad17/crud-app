# Clinic API — Week 3 Portfolio Project

Healthcare-flavoured CRUD API built for CureMD / Systems Ltd / NetSol interviews.

## Architecture

```
HTTP Request
    ↓
Controllers  (api/doctors, api/patients, api/appointments)
    ↓
Services     (business rules, DTO mapping)
    ↓
Repositories
    ├── EF Core  → Doctors, Patients, Appointments CRUD
    └── ADO.NET  → GET /api/appointments/report  (JOIN-heavy)
    ↓
SQLite (clinic.db)  — swap ConnectionStrings for SQL Server in prod
```

**Why both EF and ADO.NET?** CureMD-style teams often keep stored procedures / hand SQL for reports while using an ORM for CRUD. You can defend this in interviews.

## Run

```bash
cd week3/ClinicApi
dotnet run
# Swagger UI: http://localhost:<port>/swagger
```

```bash
# Tests
dotnet test week3/ClinicApi.Tests
```

## Sample calls

```bash
curl http://localhost:5xxx/api/doctors?page=1&pageSize=10
curl http://localhost:5xxx/api/patients
curl -X POST http://localhost:5xxx/api/patients \
  -H 'Content-Type: application/json' \
  -d '{"fullName":"Ali Raza","dateOfBirth":"1998-04-01","phone":"03001112233"}'
curl 'http://localhost:5xxx/api/appointments/report?doctorId=1'
```

## Endpoints

| Method | Path | Notes |
|--------|------|-------|
| GET/POST | `/api/doctors` | pagination + specialty filter |
| GET/PUT/DELETE | `/api/doctors/{id}` | |
| GET/POST | `/api/patients` | pagination |
| GET/PUT/DELETE | `/api/patients/{id}` | |
| GET/POST | `/api/appointments` | pagination |
| GET | `/api/appointments/report` | **ADO.NET JOIN report** |
| PATCH | `/api/appointments/{id}/status` | Scheduled/Completed/Cancelled |
| DELETE | `/api/appointments/{id}` | |

## 5-minute project defence (memorize)

1. **What it is:** REST API for clinic patients, doctors, appointments.
2. **Layers:** Controller → Service → Repository with interfaces + DI (testable).
3. **Why ADO.NET for report:** multi-table JOIN report; shows I can write parameterized SQL like legacy .NET shops.
4. **Bug I fixed / design choice:** reject past appointments and future DOB in the service layer.
5. **Next improvement:** auth (JWT), SQL Server + real stored proc, FluentValidation, integration tests.

## Optional SQL Server (Docker)

```bash
docker compose -f week3/docker-compose.yml up -d
# then change ConnectionStrings:ClinicDb to:
# Server=localhost,1433;Database=Clinic;User Id=sa;Password=Your_strong_Password123;TrustServerCertificate=True
# and swap UseSqlite → UseSqlServer + Microsoft.Data.SqlClient
```

## Coding drill (Week 3)

10 more problems — see `week3/CodingDrill3/`.
