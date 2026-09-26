using ClinicApi.Contracts;
using ClinicApi.Domain;

namespace ClinicApi.Repositories;

public interface IDoctorRepository
{
    Task<(IReadOnlyList<Doctor> Items, int Total)> GetPagedAsync(int page, int pageSize, string? specialty, CancellationToken ct);
    Task<Doctor?> GetByIdAsync(int id, CancellationToken ct);
    Task<Doctor> AddAsync(Doctor doctor, CancellationToken ct);
    Task UpdateAsync(Doctor doctor, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}

public interface IPatientRepository
{
    Task<(IReadOnlyList<Patient> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<Patient?> GetByIdAsync(int id, CancellationToken ct);
    Task<Patient> AddAsync(Patient patient, CancellationToken ct);
    Task UpdateAsync(Patient patient, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}

public interface IAppointmentRepository
{
    Task<(IReadOnlyList<Appointment> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<Appointment?> GetByIdAsync(int id, CancellationToken ct);
    Task<Appointment> AddAsync(Appointment appointment, CancellationToken ct);
    Task UpdateAsync(Appointment appointment, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}

/// <summary>Raw ADO.NET report path — mirrors CureMD-style stored-proc / hand SQL access.</summary>
public interface IAppointmentReportRepository
{
    Task<IReadOnlyList<AppointmentReportRow>> GetReportAsync(int? doctorId, DateTime? from, DateTime? to, CancellationToken ct);
}
