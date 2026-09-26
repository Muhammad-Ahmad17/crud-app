using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Specialty { get; set; } = "";
    public List<Appointment> Appointments { get; set; } = new();
}

public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string Status { get; set; } = "Scheduled";
}

public class ClinicDbContext : DbContext
{
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=efcore-demo.db");
}
