using ClinicApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ClinicDbContext db)
    {
        if (await db.Doctors.AnyAsync())
            return;

        var doctors = new[]
        {
            new Doctor { FullName = "Dr. Ayesha Khan", Specialty = "Cardiology", Email = "ayesha@clinic.local" },
            new Doctor { FullName = "Dr. Bilal Ahmed", Specialty = "Dermatology", Email = "bilal@clinic.local" },
            new Doctor { FullName = "Dr. Sana Malik", Specialty = "Pediatrics", Email = "sana@clinic.local" }
        };
        db.Doctors.AddRange(doctors);

        var patients = new[]
        {
            new Patient { FullName = "Sara Ali", DateOfBirth = new DateOnly(1994, 5, 12), Phone = "03001234567" },
            new Patient { FullName = "Omar Farooq", DateOfBirth = new DateOnly(1987, 11, 3), Phone = "03007654321" },
            new Patient { FullName = "Noor Hassan", DateOfBirth = new DateOnly(2001, 2, 20), Email = "noor@example.com" }
        };
        db.Patients.AddRange(patients);
        await db.SaveChangesAsync();

        db.Appointments.AddRange(
            new Appointment
            {
                PatientId = patients[0].Id,
                DoctorId = doctors[0].Id,
                ScheduledAt = DateTime.UtcNow.AddDays(2),
                Status = AppointmentStatus.Scheduled,
                Notes = "Follow-up ECG"
            },
            new Appointment
            {
                PatientId = patients[1].Id,
                DoctorId = doctors[0].Id,
                ScheduledAt = DateTime.UtcNow.AddDays(-5),
                Status = AppointmentStatus.Completed
            },
            new Appointment
            {
                PatientId = patients[2].Id,
                DoctorId = doctors[2].Id,
                ScheduledAt = DateTime.UtcNow.AddDays(1),
                Status = AppointmentStatus.Scheduled
            });
        await db.SaveChangesAsync();
    }
}
