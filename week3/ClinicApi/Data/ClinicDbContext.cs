using ClinicApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Data;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(e =>
        {
            e.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Specialty).HasMaxLength(80).IsRequired();
            e.Property(x => x.Email).HasMaxLength(120);
        });

        modelBuilder.Entity<Patient>(e =>
        {
            e.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(30);
            e.Property(x => x.Email).HasMaxLength(120);
        });

        modelBuilder.Entity<Appointment>(e =>
        {
            e.HasOne(x => x.Patient).WithMany(p => p.Appointments).HasForeignKey(x => x.PatientId);
            e.HasOne(x => x.Doctor).WithMany(d => d.Appointments).HasForeignKey(x => x.DoctorId);
            e.HasIndex(x => x.DoctorId);
            e.HasIndex(x => x.ScheduledAt);
            e.Property(x => x.Notes).HasMaxLength(500);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        });
    }
}
