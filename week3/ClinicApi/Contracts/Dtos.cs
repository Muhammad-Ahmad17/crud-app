using System.ComponentModel.DataAnnotations;

namespace ClinicApi.Contracts;

public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}

public class CreateDoctorDto
{
    [Required, MinLength(2), MaxLength(100)]
    public string FullName { get; set; } = "";

    [Required, MinLength(2), MaxLength(80)]
    public string Specialty { get; set; } = "";

    [EmailAddress]
    public string? Email { get; set; }
}

public class UpdateDoctorDto : CreateDoctorDto { }

public record DoctorDto(int Id, string FullName, string Specialty, string? Email);

public class CreatePatientDto
{
    [Required, MinLength(2), MaxLength(100)]
    public string FullName { get; set; } = "";

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}

public class UpdatePatientDto : CreatePatientDto { }

public record PatientDto(int Id, string FullName, DateOnly DateOfBirth, string? Phone, string? Email);

public class CreateAppointmentDto
{
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    [Required]
    public DateTime ScheduledAt { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}

public class UpdateAppointmentStatusDto
{
    [Required]
    public string Status { get; set; } = "Scheduled";
}

public record AppointmentDto(
    int Id,
    int PatientId,
    string PatientName,
    int DoctorId,
    string DoctorName,
    DateTime ScheduledAt,
    string Status,
    string? Notes);

public record AppointmentReportRow(
    int AppointmentId,
    string PatientName,
    string DoctorName,
    string Specialty,
    DateTime ScheduledAt,
    string Status);
