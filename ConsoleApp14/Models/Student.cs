namespace UniversitySystem.Models;

public class Student : Person
{
    public int YearOfStudy { get; set; }
    public Faculty Faculty { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();

    public Student(string name, string surname, int age, string email, int yearOfStudy, Faculty faculty)
        : base(name, surname, age, email)
    {
        YearOfStudy = yearOfStudy;
        Faculty = faculty;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"  Студент: {Surname} {Name}");
        Console.WriteLine($"  ID: {Id}");
        Console.WriteLine($"  Возраст: {Age}");
        Console.WriteLine($"  Email: {Email}");
        Console.WriteLine($"  Факультет: {Faculty}");
        Console.WriteLine($"  Курс: {YearOfStudy}");
        Console.WriteLine($"  Записан на {Courses.Count} курс(ов)");
    }
}
