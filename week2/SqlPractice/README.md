# Week 2 SQL Practice (whiteboard + SQLZoo / LeetCode Database)

Practice these on paper first, then run against any SQL engine.

## Schema (healthcare-flavoured — reuse in Week 3)

```sql
CREATE TABLE Doctors (
  Id INTEGER PRIMARY KEY,
  FullName TEXT NOT NULL,
  Specialty TEXT NOT NULL
);

CREATE TABLE Patients (
  Id INTEGER PRIMARY KEY,
  FullName TEXT NOT NULL,
  DateOfBirth TEXT NOT NULL,
  Phone TEXT
);

CREATE TABLE Appointments (
  Id INTEGER PRIMARY KEY,
  PatientId INTEGER NOT NULL REFERENCES Patients(Id),
  DoctorId INTEGER NOT NULL REFERENCES Doctors(Id),
  ScheduledAt TEXT NOT NULL,
  Status TEXT NOT NULL -- Scheduled | Completed | Cancelled
);
```

## Must-know queries (write from memory)

### 1. INNER JOIN — appointments with patient + doctor names
```sql
SELECT a.Id, p.FullName AS Patient, d.FullName AS Doctor, a.ScheduledAt, a.Status
FROM Appointments a
INNER JOIN Patients p ON p.Id = a.PatientId
INNER JOIN Doctors d ON d.Id = a.DoctorId;
```

### 2. LEFT JOIN — doctors with zero appointments still shown
```sql
SELECT d.FullName, COUNT(a.Id) AS AppointmentCount
FROM Doctors d
LEFT JOIN Appointments a ON a.DoctorId = d.Id
GROUP BY d.Id, d.FullName
ORDER BY AppointmentCount DESC;
```

### 3. GROUP BY / HAVING — doctors with more than 2 completed visits
```sql
SELECT d.FullName, COUNT(*) AS Completed
FROM Appointments a
JOIN Doctors d ON d.Id = a.DoctorId
WHERE a.Status = 'Completed'
GROUP BY d.Id, d.FullName
HAVING COUNT(*) > 2;
```

### 4. Subquery — patients who never had an appointment
```sql
SELECT p.*
FROM Patients p
WHERE p.Id NOT IN (SELECT DISTINCT PatientId FROM Appointments);
```

### 5. Index (interview: what/why)
```sql
CREATE INDEX IX_Appointments_DoctorId ON Appointments(DoctorId);
CREATE INDEX IX_Appointments_ScheduledAt ON Appointments(ScheduledAt);
```
Indexes speed lookups/joins/filters on that column; they cost write time + storage.

### 6. Stored procedure idea (SQL Server syntax — CureMD world)
```sql
-- SQL Server example (conceptual; SQLite has no procs)
CREATE PROCEDURE dbo.usp_AppointmentsByDoctor
  @DoctorId INT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS
BEGIN
  SELECT a.*, p.FullName AS PatientName
  FROM Appointments a
  INNER JOIN Patients p ON p.Id = a.PatientId
  WHERE a.DoctorId = @DoctorId
    AND a.ScheduledAt BETWEEN @FromDate AND @ToDate;
END
```

SQLite substitute used in demos: parameterized query or a VIEW named `vw_AppointmentsReport`.

## Drills

- [ ] Rewrite JOIN #1 on whiteboard in < 3 min
- [ ] Explain INNER vs LEFT with an example row
- [ ] Explain why `@param` / `?` prevents SQL injection
- [ ] Complete SQLZoo JOINs section
- [ ] 5 LeetCode Database Easy problems
