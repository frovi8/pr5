namespace UniversitySystem.Models;

public class Course
{
    private static int _counter = 0;

    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxStudents { get; set; }
    public Teacher? Teacher { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();

    public Course(string name, string description, int maxStudents)
    {
        _counter++;
        Id = _counter;
        Name = name;
        Description = description;
        MaxStudents = maxStudents;
    }

    public void PrintInfo()
    {
        string teacherName = Teacher != null ? Teacher.ToString() : "не назначен";
        Console.WriteLine($"  Курс: {Name}");
        Console.WriteLine($"  ID: {Id}");
        Console.WriteLine($"  Описание: {Description}");
        Console.WriteLine($"  Преподаватель: {teacherName}");
        Console.WriteLine($"  Студентов: {Students.Count}/{MaxStudents}");
    }
}
