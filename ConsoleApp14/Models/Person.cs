namespace UniversitySystem.Models;

public enum Faculty
{
    Информатика,
    Математика,
    Физика,
    Экономика,
    Юриспруденция,
    Филология
}

public enum AcademicDegree
{
    Нет,
    КандидатНаук,
    ДокторНаук,
    Профессор
}

public abstract class Person
{
    private static int _counter = 0;

    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public Person(string name, string surname, int age, string email)
    {
        _counter++;
        Id = _counter;
        Name = name;
        Surname = surname;
        Age = age;
        Email = email;
    }

    public abstract void PrintInfo();

    public override string ToString()
    {
        return $"{Surname} {Name} (ID: {Id})";
    }
}
