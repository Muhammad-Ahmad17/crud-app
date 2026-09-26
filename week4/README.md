# Week 4 — Interview Prep + Applications

## Mock coding rounds (every other day)

Timer: **45 minutes**, 3 easy problems, **no AI / no notes**.

Use LeetCode Easy or re-solve from CodingDrill 1–40 with a blank file.

Log: date, problems, solved count, time — in `progress/progress.md`.

## Technical Q&A (say out loud — then check answers below)

### OOP
1. **Four pillars?** Encapsulation, Abstraction, Inheritance, Polymorphism — give a code example each from BankAccount.
2. **Interface vs abstract class?** Interface = contract, multiple. Abstract = shared code + state, single inheritance.
3. **Value vs reference types?** struct/int/bool on stack (copied); class/string/array on heap (reference copied).

### .NET / ASP.NET
4. **IEnumerable vs IQueryable?** In-memory vs expression→SQL.
5. **async/await?** Free the thread while waiting on I/O; doesn't create threads by itself.
6. **DI lifetimes?** Singleton (one), Scoped (per request), Transient (every inject).
7. **Request pipeline?** Middleware → routing → model bind → action filter → controller → result.

### SQL
8. **Stored procedure?** Named, precompiled SQL on the server; CureMD-heavy.
9. **Index?** Speeds read/filter/join; costs writes + space.
10. **INNER vs LEFT JOIN?** Inner = matches only; Left = all left + matching right (nulls).
11. **ACID?** Atomicity, Consistency, Isolation, Durability.

### REST
12. **Status codes?** 200 OK, 201 Created, 204 No Content, 400 Bad Request, 404 Not Found, 500 Server Error.

## 5-minute ClinicApi defence (script)

> "I built a clinic REST API in ASP.NET Core with layered architecture: Controllers call Services which call Repositories. CRUD for doctors, patients, and appointments uses EF Core. The appointments report endpoint uses raw ADO.NET with parameterized JOINs — the same pattern product companies like CureMD use with stored procedures on legacy schemas. Services enforce rules like no past appointments. I have xUnit tests with mocked repositories, Swagger, and pagination. Next I'd add JWT auth and SQL Server."

Practice until you can say this without reading.

## HR + focus group (Systems Ltd)

**60-second intro:**
> "I'm [Name], final-year [CS/SE] student. I know Node REST APIs and spent the last month building the same patterns in C# and ASP.NET Core, including ADO.NET for SQL-heavy work. I want to join a product company where I can own features end-to-end and grow on enterprise .NET."

**Why product companies:** long-lived products, deeper domain, clearer ownership vs pure outsourcing ticket grind.

**Focus group tip:** (1) restate the problem, (2) propose 2 options, (3) pick one with a reason, (4) assign who speaks.

## CV (one page) — edit `week4/CV_TEMPLATE.md`

Skills line: `C#, ASP.NET Core, ADO.NET, EF Core, SQL, REST, Git, Node.js`

## Apply checklist

- [ ] CureMD — Associate Software Engineer (careers / campus drive)
- [ ] Systems Limited — MTO / campus recruitment
- [ ] NetSol — careers + NIAI backup
- [ ] Contour Software
- [ ] Techlogix
- [ ] i2c
- [ ] Arbisoft
- [ ] 10Pearls
- [ ] Confiz
- [ ] Devsinc

Ask your **university placement office** when CureMD / Systems drives are scheduled.

**CureMD note:** confirm bond terms (reports mention ~2 years / 1M PKR) before accepting.

## CCAT — daily this week

15 minutes timed. Accuracy on first ~30 > finishing all 50.
