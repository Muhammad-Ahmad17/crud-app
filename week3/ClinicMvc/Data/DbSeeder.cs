using ClinicMvc.Data;
using ClinicMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvc.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Patients.AnyAsync())
            return;

        db.Patients.AddRange(
            new Patient
            {
                FullName = "Sara Ali",
                DateOfBirth = new DateOnly(1994, 5, 12),
                Phone = "03001234567",
                Email = "sara@example.com"
            },
            new Patient
            {
                FullName = "Omar Farooq",
                DateOfBirth = new DateOnly(1987, 11, 3),
                Phone = "03007654321",
                Notes = "Follow-up next month"
            },
            new Patient
            {
                FullName = "Noor Hassan",
                DateOfBirth = new DateOnly(2001, 2, 20),
                Email = "noor@example.com"
            });

        await db.SaveChangesAsync();
    }
}
