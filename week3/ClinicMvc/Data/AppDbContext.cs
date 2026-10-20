using ClinicMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvc.Data;

/// <summary>
/// EF Core DbContext = database gateway used by Controllers.
/// In ClinicApi this lives behind a Repository; here the Controller
/// talks to DbContext directly so MVC stays easy to see.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
}
