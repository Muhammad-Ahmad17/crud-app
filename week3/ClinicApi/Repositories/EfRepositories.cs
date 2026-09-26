using ClinicApi.Data;
using ClinicApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ClinicDbContext _db;
    public DoctorRepository(ClinicDbContext db) => _db = db;

    public async Task<(IReadOnlyList<Doctor> Items, int Total)> GetPagedAsync(int page, int pageSize, string? specialty, CancellationToken ct)
    {
        var q = _db.Doctors.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(specialty))
            q = q.Where(d => d.Specialty == specialty);

        var total = await q.CountAsync(ct);
        var items = await q.OrderBy(d => d.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return (items, total);
    }

    public Task<Doctor?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Doctors.FirstOrDefaultAsync(d => d.Id == id, ct);

    public async Task<Doctor> AddAsync(Doctor doctor, CancellationToken ct)
    {
        _db.Doctors.Add(doctor);
        await _db.SaveChangesAsync(ct);
        return doctor;
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken ct)
    {
        _db.Doctors.Update(doctor);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Doctors.FindAsync([id], ct);
        if (entity is null) return false;
        _db.Doctors.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}

public class PatientRepository : IPatientRepository
{
    private readonly ClinicDbContext _db;
    public PatientRepository(ClinicDbContext db) => _db = db;

    public async Task<(IReadOnlyList<Patient> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Patients.AsNoTracking();
        var total = await q.CountAsync(ct);
        var items = await q.OrderBy(p => p.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return (items, total);
    }

    public Task<Patient?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Patients.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<Patient> AddAsync(Patient patient, CancellationToken ct)
    {
        _db.Patients.Add(patient);
        await _db.SaveChangesAsync(ct);
        return patient;
    }

    public async Task UpdateAsync(Patient patient, CancellationToken ct)
    {
        _db.Patients.Update(patient);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Patients.FindAsync([id], ct);
        if (entity is null) return false;
        _db.Patients.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ClinicDbContext _db;
    public AppointmentRepository(ClinicDbContext db) => _db = db;

    public async Task<(IReadOnlyList<Appointment> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Appointments.AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Doctor);
        var total = await q.CountAsync(ct);
        var items = await q.OrderByDescending(a => a.ScheduledAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return (items, total);
    }

    public Task<Appointment?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken ct)
    {
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync(ct);
        return await GetByIdAsync(appointment.Id, ct) ?? appointment;
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken ct)
    {
        _db.Appointments.Update(appointment);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Appointments.FindAsync([id], ct);
        if (entity is null) return false;
        _db.Appointments.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
