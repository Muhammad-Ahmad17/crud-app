using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models;

public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string? Phone { get; set; }
}

// DTO = what the API accepts/returns (not always the DB entity)
public class CreatePatientDto
{
    [Required, MinLength(2)]
    public string FullName { get; set; } = "";

    [Phone]
    public string? Phone { get; set; }
}

public class PatientDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string? Phone { get; set; }
}
