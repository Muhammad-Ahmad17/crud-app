using Microsoft.EntityFrameworkCore;
using EfCoreDemo;

// EF Core: DbContext + entities + LINQ. Prefer when schema is app-owned.
// Prefer ADO.NET/Dapper when: legacy DB, heavy stored procs, micro-optimized SQL (CureMD-style).

await using var db = new ClinicDbContext();
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();

db.Doctors.AddRange(
    new Doctor { FullName = "Dr. Ayesha Khan", Specialty = "Cardiology" },
    new Doctor { FullName = "Dr. Bilal Ahmed", Specialty = "Dermatology" });
db.Patients.AddRange(
    new Patient { FullName = "Sara Malik", DateOfBirth = new DateOnly(1995, 3, 12) },
    new Patient { FullName = "Omar Farooq", DateOfBirth = new DateOnly(1988, 7, 1) });
await db.SaveChangesAsync();

db.Appointments.Add(new Appointment
{
    PatientId = 1,
    DoctorId = 1,
    ScheduledAt = DateTime.Parse("2026-10-01T10:00:00"),
    Status = "Scheduled"
});
await db.SaveChangesAsync();

// IQueryable — translated to SQL (filtered in DB)
var cardiologyAppointments = await db.Appointments
    .Include(a => a.Patient)
    .Include(a => a.Doctor)
    .Where(a => a.Doctor!.Specialty == "Cardiology")
    .Select(a => new
    {
        a.Id,
        Patient = a.Patient!.FullName,
        Doctor = a.Doctor!.FullName,
        a.ScheduledAt,
        a.Status
    })
    .ToListAsync();

Console.WriteLine("=== EF Core LINQ query ===");
foreach (var row in cardiologyAppointments)
    Console.WriteLine($"{row.Id}: {row.Patient} -> {row.Doctor} @ {row.ScheduledAt:g} [{row.Status}]");

Console.WriteLine("\nIEnumerable vs IQueryable:");
Console.WriteLine("  IQueryable: expression tree -> SQL (filter in database)");
Console.WriteLine("  IEnumerable: in-memory LINQ after data is already loaded");
