using WebApiDemo.Models;

namespace WebApiDemo.Services;

public interface IPatientService
{
    IReadOnlyList<PatientDto> GetAll();
    PatientDto? GetById(int id);
    PatientDto Create(CreatePatientDto dto);
    bool Delete(int id);
}

public class InMemoryPatientService : IPatientService
{
    private readonly List<Patient> _patients = new();
    private int _nextId = 1;

    public IReadOnlyList<PatientDto> GetAll() =>
        _patients.Select(ToDto).ToList();

    public PatientDto? GetById(int id)
    {
        var p = _patients.FirstOrDefault(x => x.Id == id);
        return p is null ? null : ToDto(p);
    }

    public PatientDto Create(CreatePatientDto dto)
    {
        var patient = new Patient
        {
            Id = _nextId++,
            FullName = dto.FullName.Trim(),
            Phone = dto.Phone
        };
        _patients.Add(patient);
        return ToDto(patient);
    }

    public bool Delete(int id)
    {
        var p = _patients.FirstOrDefault(x => x.Id == id);
        if (p is null) return false;
        _patients.Remove(p);
        return true;
    }

    private static PatientDto ToDto(Patient p) => new()
    {
        Id = p.Id,
        FullName = p.FullName,
        Phone = p.Phone
    };
}
