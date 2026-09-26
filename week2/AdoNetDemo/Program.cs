using Microsoft.Data.Sqlite;

// ADO.NET pattern (CureMD-style): Connection -> Command -> Reader
// Uses SQLite locally. On SQL Server you'd use Microsoft.Data.SqlClient + SqlConnection.
// Parameterized queries (@name / $name / ?) prevent SQL injection.

const string DbPath = "adonet-demo.db";
if (File.Exists(DbPath)) File.Delete(DbPath);

await using var conn = new SqliteConnection($"Data Source={DbPath}");
await conn.OpenAsync();

await ExecAsync(conn, """
    CREATE TABLE Doctors (
      Id INTEGER PRIMARY KEY AUTOINCREMENT,
      FullName TEXT NOT NULL,
      Specialty TEXT NOT NULL
    );
    CREATE TABLE Patients (
      Id INTEGER PRIMARY KEY AUTOINCREMENT,
      FullName TEXT NOT NULL
    );
    CREATE TABLE Appointments (
      Id INTEGER PRIMARY KEY AUTOINCREMENT,
      PatientId INTEGER NOT NULL,
      DoctorId INTEGER NOT NULL,
      ScheduledAt TEXT NOT NULL,
      Status TEXT NOT NULL
    );
    CREATE VIEW vw_AppointmentsReport AS
      SELECT a.Id, p.FullName AS Patient, d.FullName AS Doctor,
             a.ScheduledAt, a.Status
      FROM Appointments a
      INNER JOIN Patients p ON p.Id = a.PatientId
      INNER JOIN Doctors d ON d.Id = a.DoctorId;
    """);

// Insert with parameters (NEVER string-concatenate user input)
await InsertDoctorAsync(conn, "Dr. Ayesha Khan", "Cardiology");
await InsertDoctorAsync(conn, "Dr. Bilal Ahmed", "Dermatology");
await InsertPatientAsync(conn, "Sara Malik");
await InsertPatientAsync(conn, "Omar Farooq");
await InsertAppointmentAsync(conn, patientId: 1, doctorId: 1, when: "2026-10-01T10:00:00", status: "Scheduled");
await InsertAppointmentAsync(conn, patientId: 2, doctorId: 1, when: "2026-10-02T11:00:00", status: "Completed");

Console.WriteLine("=== Appointments report (JOIN via view — SQLite stand-in for stored proc) ===");
await using (var cmd = conn.CreateCommand())
{
    // SQL Server equivalent: cmd.CommandType = CommandType.StoredProcedure; cmd.CommandText = "usp_AppointmentsByDoctor";
    cmd.CommandText = """
        SELECT * FROM vw_AppointmentsReport
        WHERE Doctor = $doctor
        """;
    cmd.Parameters.AddWithValue("$doctor", "Dr. Ayesha Khan");

    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        Console.WriteLine(
            $"#{reader.GetInt32(0)} | {reader.GetString(1)} with {reader.GetString(2)} | {reader.GetString(3)} | {reader.GetString(4)}");
    }
}

Console.WriteLine("\nWhy parameters matter: user input never becomes SQL syntax.");
Console.WriteLine("using/await using disposes connection & command (IDisposable).");

static async Task ExecAsync(SqliteConnection conn, string sql)
{
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    await cmd.ExecuteNonQueryAsync();
}

static async Task InsertDoctorAsync(SqliteConnection conn, string name, string specialty)
{
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = "INSERT INTO Doctors (FullName, Specialty) VALUES ($n, $s)";
    cmd.Parameters.AddWithValue("$n", name);
    cmd.Parameters.AddWithValue("$s", specialty);
    await cmd.ExecuteNonQueryAsync();
}

static async Task InsertPatientAsync(SqliteConnection conn, string name)
{
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = "INSERT INTO Patients (FullName) VALUES ($n)";
    cmd.Parameters.AddWithValue("$n", name);
    await cmd.ExecuteNonQueryAsync();
}

static async Task InsertAppointmentAsync(SqliteConnection conn, int patientId, int doctorId, string when, string status)
{
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = """
        INSERT INTO Appointments (PatientId, DoctorId, ScheduledAt, Status)
        VALUES ($p, $d, $when, $status)
        """;
    cmd.Parameters.AddWithValue("$p", patientId);
    cmd.Parameters.AddWithValue("$d", doctorId);
    cmd.Parameters.AddWithValue("$when", when);
    cmd.Parameters.AddWithValue("$status", status);
    await cmd.ExecuteNonQueryAsync();
}
