using ClinicApi.Contracts;
using Microsoft.Data.Sqlite;

namespace ClinicApi.Repositories;

/// <summary>
/// ADO.NET repository for JOIN-heavy reporting.
/// On SQL Server this would call: CommandType.StoredProcedure + "usp_AppointmentsReport".
/// SQLite stand-in: parameterized multi-table JOIN (same habits: parameters + SqlDataReader pattern).
/// </summary>
public class AdoAppointmentReportRepository : IAppointmentReportRepository
{
    private readonly string _connectionString;

    public AdoAppointmentReportRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("ClinicDb")
            ?? throw new InvalidOperationException("Missing ConnectionStrings:ClinicDb");
    }

    public async Task<IReadOnlyList<AppointmentReportRow>> GetReportAsync(
        int? doctorId, DateTime? from, DateTime? to, CancellationToken ct)
    {
        var rows = new List<AppointmentReportRow>();

        await using var conn = new SqliteConnection(_connectionString);
        await conn.OpenAsync(ct);

        await using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            SELECT a.Id,
                   p.FullName,
                   d.FullName,
                   d.Specialty,
                   a.ScheduledAt,
                   a.Status
            FROM Appointments a
            INNER JOIN Patients p ON p.Id = a.PatientId
            INNER JOIN Doctors d ON d.Id = a.DoctorId
            WHERE ($doctorId IS NULL OR a.DoctorId = $doctorId)
              AND ($from IS NULL OR a.ScheduledAt >= $from)
              AND ($to IS NULL OR a.ScheduledAt <= $to)
            ORDER BY a.ScheduledAt DESC;
            """;
        cmd.Parameters.AddWithValue("$doctorId", doctorId.HasValue ? doctorId.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("$from", from.HasValue ? from.Value.ToString("O") : DBNull.Value);
        cmd.Parameters.AddWithValue("$to", to.HasValue ? to.Value.ToString("O") : DBNull.Value);

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            rows.Add(new AppointmentReportRow(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                DateTime.Parse(reader.GetString(4), null, System.Globalization.DateTimeStyles.RoundtripKind),
                reader.GetString(5)));
        }

        return rows;
    }
}
