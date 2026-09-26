using ClinicApi.Contracts;
using ClinicApi.Domain;
using ClinicApi.Exceptions;
using ClinicApi.Repositories;

namespace ClinicApi.Services;

public interface IDoctorService
{
    Task<PagedResult<DoctorDto>> GetPagedAsync(int page, int pageSize, string? specialty, CancellationToken ct);
    Task<DoctorDto> GetByIdAsync(int id, CancellationToken ct);
    Task<DoctorDto> CreateAsync(CreateDoctorDto dto, CancellationToken ct);
    Task<DoctorDto> UpdateAsync(int id, UpdateDoctorDto dto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}

public interface IPatientService
{
    Task<PagedResult<PatientDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<PatientDto> GetByIdAsync(int id, CancellationToken ct);
    Task<PatientDto> CreateAsync(CreatePatientDto dto, CancellationToken ct);
    Task<PatientDto> UpdateAsync(int id, UpdatePatientDto dto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}

public interface IAppointmentService
{
    Task<PagedResult<AppointmentDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<AppointmentDto> GetByIdAsync(int id, CancellationToken ct);
    Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto, CancellationToken ct);
    Task<AppointmentDto> UpdateStatusAsync(int id, UpdateAppointmentStatusDto dto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<AppointmentReportRow>> GetReportAsync(int? doctorId, DateTime? from, DateTime? to, CancellationToken ct);
}

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;
    public DoctorService(IDoctorRepository repo) => _repo = repo;

    public async Task<PagedResult<DoctorDto>> GetPagedAsync(int page, int pageSize, string? specialty, CancellationToken ct)
    {
        (page, pageSize) = Normalize(page, pageSize);
        var (items, total) = await _repo.GetPagedAsync(page, pageSize, specialty, ct);
        return new PagedResult<DoctorDto>(items.Select(Map).ToList(), page, pageSize, total);
    }

    public async Task<DoctorDto> GetByIdAsync(int id, CancellationToken ct)
    {
        var d = await _repo.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Doctor {id} not found.");
        return Map(d);
    }

    public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto, CancellationToken ct)
    {
        var entity = new Doctor
        {
            FullName = dto.FullName.Trim(),
            Specialty = dto.Specialty.Trim(),
            Email = dto.Email
        };
        return Map(await _repo.AddAsync(entity, ct));
    }

    public async Task<DoctorDto> UpdateAsync(int id, UpdateDoctorDto dto, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Doctor {id} not found.");
        entity.FullName = dto.FullName.Trim();
        entity.Specialty = dto.Specialty.Trim();
        entity.Email = dto.Email;
        await _repo.UpdateAsync(entity, ct);
        return Map(entity);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        if (!await _repo.DeleteAsync(id, ct))
            throw new NotFoundException($"Doctor {id} not found.");
    }

    private static DoctorDto Map(Doctor d) => new(d.Id, d.FullName, d.Specialty, d.Email);

    internal static (int page, int pageSize) Normalize(int page, int pageSize)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;
        return (page, pageSize);
    }
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    public PatientService(IPatientRepository repo) => _repo = repo;

    public async Task<PagedResult<PatientDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        (page, pageSize) = DoctorService.Normalize(page, pageSize);
        var (items, total) = await _repo.GetPagedAsync(page, pageSize, ct);
        return new PagedResult<PatientDto>(items.Select(Map).ToList(), page, pageSize, total);
    }

    public async Task<PatientDto> GetByIdAsync(int id, CancellationToken ct)
    {
        var p = await _repo.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Patient {id} not found.");
        return Map(p);
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto, CancellationToken ct)
    {
        if (dto.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BusinessException("Date of birth cannot be in the future.");

        var entity = new Patient
        {
            FullName = dto.FullName.Trim(),
            DateOfBirth = dto.DateOfBirth,
            Phone = dto.Phone,
            Email = dto.Email
        };
        return Map(await _repo.AddAsync(entity, ct));
    }

    public async Task<PatientDto> UpdateAsync(int id, UpdatePatientDto dto, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Patient {id} not found.");
        if (dto.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BusinessException("Date of birth cannot be in the future.");

        entity.FullName = dto.FullName.Trim();
        entity.DateOfBirth = dto.DateOfBirth;
        entity.Phone = dto.Phone;
        entity.Email = dto.Email;
        await _repo.UpdateAsync(entity, ct);
        return Map(entity);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        if (!await _repo.DeleteAsync(id, ct))
            throw new NotFoundException($"Patient {id} not found.");
    }

    private static PatientDto Map(Patient p) =>
        new(p.Id, p.FullName, p.DateOfBirth, p.Phone, p.Email);
}

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;
    private readonly IPatientRepository _patients;
    private readonly IDoctorRepository _doctors;
    private readonly IAppointmentReportRepository _report;

    public AppointmentService(
        IAppointmentRepository repo,
        IPatientRepository patients,
        IDoctorRepository doctors,
        IAppointmentReportRepository report)
    {
        _repo = repo;
        _patients = patients;
        _doctors = doctors;
        _report = report;
    }

    public async Task<PagedResult<AppointmentDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        (page, pageSize) = DoctorService.Normalize(page, pageSize);
        var (items, total) = await _repo.GetPagedAsync(page, pageSize, ct);
        return new PagedResult<AppointmentDto>(items.Select(Map).ToList(), page, pageSize, total);
    }

    public async Task<AppointmentDto> GetByIdAsync(int id, CancellationToken ct)
    {
        var a = await _repo.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Appointment {id} not found.");
        return Map(a);
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto, CancellationToken ct)
    {
        _ = await _patients.GetByIdAsync(dto.PatientId, ct)
            ?? throw new BusinessException($"Patient {dto.PatientId} does not exist.");
        _ = await _doctors.GetByIdAsync(dto.DoctorId, ct)
            ?? throw new BusinessException($"Doctor {dto.DoctorId} does not exist.");

        if (dto.ScheduledAt < DateTime.UtcNow.AddMinutes(-5))
            throw new BusinessException("Cannot schedule appointments in the past.");

        var entity = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            ScheduledAt = DateTime.SpecifyKind(dto.ScheduledAt, DateTimeKind.Utc),
            Notes = dto.Notes,
            Status = AppointmentStatus.Scheduled
        };
        return Map(await _repo.AddAsync(entity, ct));
    }

    public async Task<AppointmentDto> UpdateStatusAsync(int id, UpdateAppointmentStatusDto dto, CancellationToken ct)
    {
        if (!Enum.TryParse<AppointmentStatus>(dto.Status, ignoreCase: true, out var status))
            throw new BusinessException("Status must be Scheduled, Completed, or Cancelled.");

        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Appointment {id} not found.");
        entity.Status = status;
        await _repo.UpdateAsync(entity, ct);
        return Map(await _repo.GetByIdAsync(id, ct) ?? entity);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        if (!await _repo.DeleteAsync(id, ct))
            throw new NotFoundException($"Appointment {id} not found.");
    }

    public Task<IReadOnlyList<AppointmentReportRow>> GetReportAsync(
        int? doctorId, DateTime? from, DateTime? to, CancellationToken ct) =>
        _report.GetReportAsync(doctorId, from, to, ct);

    private static AppointmentDto Map(Appointment a) => new(
        a.Id,
        a.PatientId,
        a.Patient?.FullName ?? "",
        a.DoctorId,
        a.Doctor?.FullName ?? "",
        a.ScheduledAt,
        a.Status.ToString(),
        a.Notes);
}
