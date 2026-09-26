namespace ClinicApi.Domain;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Specialty { get; set; } = "";
    public string? Email { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}

public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}

public enum AppointmentStatus
{
    Scheduled = 0,
    Completed = 1,
    Cancelled = 2
}

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public DateTime ScheduledAt { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string? Notes { get; set; }
}
