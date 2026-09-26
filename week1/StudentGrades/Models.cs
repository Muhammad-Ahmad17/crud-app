namespace StudentGrades;

public class Student
{
    public string Id { get; }
    public string Name { get; }
    public List<GradeEntry> Grades { get; } = new();

    public Student(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public double Average => Grades.Count == 0 ? 0 : Grades.Average(g => g.Score);
}

public record GradeEntry(string Course, int Score);

public interface IGradeBook
{
    void AddStudent(Student student);
    void RecordGrade(string studentId, string course, int score);
    IEnumerable<string> GetClassReport();
    Dictionary<string, double> GetCourseAverages();
    Student? GetTopStudent();
}

public class GradeBook : IGradeBook
{
    private readonly Dictionary<string, Student> _students = new();

    public void AddStudent(Student student) => _students[student.Id] = student;

    public void RecordGrade(string studentId, string course, int score)
    {
        if (score is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(score), "Score must be 0-100.");
        Get(studentId).Grades.Add(new GradeEntry(course, score));
    }

    public IEnumerable<string> GetClassReport() =>
        _students.Values
            .OrderByDescending(s => s.Average)
            .Select(s => $"{s.Name}: avg={s.Average:F1} | " +
                         string.Join(", ", s.Grades.Select(g => $"{g.Course}={g.Score}")));

    public Dictionary<string, double> GetCourseAverages() =>
        _students.Values
            .SelectMany(s => s.Grades)
            .GroupBy(g => g.Course)
            .ToDictionary(g => g.Key, g => g.Average(x => x.Score));

    public Student? GetTopStudent() =>
        _students.Values.OrderByDescending(s => s.Average).FirstOrDefault();

    private Student Get(string id) =>
        _students.TryGetValue(id, out var s)
            ? s
            : throw new KeyNotFoundException($"Student {id} not found.");
}
