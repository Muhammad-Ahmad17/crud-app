using StudentGrades;

IGradeBook book = new GradeBook();

book.AddStudent(new Student("S1", "Fatima"));
book.AddStudent(new Student("S2", "Hassan"));
book.AddStudent(new Student("S3", "Noor"));

book.RecordGrade("S1", "OOP", 88);
book.RecordGrade("S1", "DB", 92);
book.RecordGrade("S2", "OOP", 75);
book.RecordGrade("S2", "DB", 68);
book.RecordGrade("S3", "OOP", 95);
book.RecordGrade("S3", "DB", 90);

Console.WriteLine("=== Class Report ===");
foreach (var line in book.GetClassReport())
    Console.WriteLine(line);

Console.WriteLine("\n=== Course Averages (LINQ GroupBy) ===");
foreach (var avg in book.GetCourseAverages())
    Console.WriteLine($"  {avg.Key}: {avg.Value:F1}");

Console.WriteLine($"\nTop student: {book.GetTopStudent()?.Name ?? "N/A"}");
