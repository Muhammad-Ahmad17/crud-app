using System.ComponentModel.DataAnnotations;

namespace ClinicMvc.Models;

/// <summary>
/// MODEL — the data shape + validation rules.
/// Controllers load/save this; Views display/edit it.
/// </summary>
public class Patient
{
    public int Id { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    [Display(Name = "Full name")]
    public string FullName { get; set; } = "";

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateOnly DateOfBirth { get; set; }

    [Phone]
    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(200)]
    public string? Notes { get; set; }
}
