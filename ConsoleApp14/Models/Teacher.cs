namespace UniversitySystem.Models;

public class Teacher : Person
{
    public string Department { get; set; }
    public AcademicDegree Degree { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();

    public Teacher(string name, string surname, int age, string email, string department, AcademicDegree degree)
        : base(name, surname, age, email)
    {
        Department = department;
        Degree = degree;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"  Преподаватель: {Surname} {Name}");
        Console.WriteLine($"  ID: {Id}");
        Console.WriteLine($"  Возраст: {Age}");
        Console.WriteLine($"  Email: {Email}");
        Console.WriteLine($"  Кафедра: {Department}");
        Console.WriteLine($"  Степень: {Degree}");
        Console.WriteLine($"  Ведет {Courses.Count} курс(ов)");
    }
}
