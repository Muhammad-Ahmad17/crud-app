# Week 2 — SQL + ADO.NET + EF Core + ASP.NET Core Web API

## Run demos

```bash
dotnet run --project week2/AdoNetDemo
dotnet run --project week2/EfCoreDemo
dotnet run --project week2/WebApiDemo
# then: curl http://localhost:5xxx/api/patients
dotnet run --project week2/CodingDrill2
```

See `SqlPractice/README.md` for JOIN / GROUP BY / stored-proc drills.

## Express → ASP.NET Core cheat sheet

| Node / Express | ASP.NET Core |
|----------------|--------------|
| `express()` + `app.listen` | `WebApplication.CreateBuilder` + `app.Run()` |
| `app.use(express.json())` | built-in model binding |
| `router.get/post` | `[HttpGet]` / `[HttpPost]` on controller |
| `req.params.id` | action parameter `int id` |
| `res.status(404).json()` | `NotFound()` / `Ok(dto)` |
| `require` modules | DI: `builder.Services.AddScoped/Singleton` |
| `process.env` | `appsettings.json` + `IConfiguration` |
| middleware | `app.Use...` pipeline |
| async handlers | `async Task<ActionResult>` + `await` |

## When ADO.NET vs EF Core (interview gold for CureMD)

- **ADO.NET / Dapper**: legacy schemas, stored procedures everywhere, hand-tuned SQL, maximum control
- **EF Core**: new greenfield CRUD, migrations owned by app, rapid development
- Teams often mix both — Week 3 portfolio does exactly that

## Coding drill (problems 16–30)

Implement stubs in `CodingDrill2` — hash maps, two pointers, sorting basics.

## CCAT

1 timed set every other day. Log in `progress/progress.md`.
