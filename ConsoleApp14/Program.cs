using UniversitySystem.Models;

namespace UniversitySystem;

class Program
{
    static List<Student> students = new List<Student>();
    static List<Teacher> teachers = new List<Teacher>();
    static List<Course> courses = new List<Course>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Система управления университетом");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Добавить преподавателя");
            Console.WriteLine("3. Создать курс");
            Console.WriteLine("4. Записать студента на курс");
            Console.WriteLine("5. Назначить преподавателя на курс");
            Console.WriteLine("6. Информация о студенте");
            Console.WriteLine("7. Информация о преподавателе");
            Console.WriteLine("8. Информация о курсе");
            Console.WriteLine("9. Все студенты");
            Console.WriteLine("10. Все преподаватели");
            Console.WriteLine("11. Все курсы");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите действие: ");

            string? input = Console.ReadLine();
            Console.Clear();

            try
            {
                switch (input)
                {
                    case "1": AddStudent(); break;
                    case "2": AddTeacher(); break;
                    case "3": AddCourse(); break;
                    case "4": EnrollStudent(); break;
                    case "5": AssignTeacher(); break;
                    case "6": ShowStudent(); break;
                    case "7": ShowTeacher(); break;
                    case "8": ShowCourse(); break;
                    case "9": ShowAllStudents(); break;
                    case "10": ShowAllTeachers(); break;
                    case "11": ShowAllCourses(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный пункт меню!"); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }


    static string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s))
                return s.Trim();
            Console.WriteLine("Значение не может быть пустым!");
        }
    }

    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int val) && val >= min && val <= max)
                return val;
            Console.WriteLine($"Введите число от {min} до {max}!");
        }
    }

    static string ReadEmail(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s) && s.Contains('@') && s.Contains('.'))
                return s.Trim();
            Console.WriteLine("Неправильный email!");
        }
    }

    static T ReadEnum<T>(string prompt) where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        Console.WriteLine(prompt);
        for (int i = 0; i < values.Length; i++)
            Console.WriteLine($"  {i + 1}. {values[i]}");

        int choice = ReadInt("Номер: ", 1, values.Length);
        return values[choice - 1];
    }


    static void AddStudent()
    {
        Console.WriteLine("Добавление студента");
        string name = ReadString("Имя: ");
        string surname = ReadString("Фамилия: ");
        int age = ReadInt("Возраст: ", 16, 100);
        string email = ReadEmail("Email: ");
        int year = ReadInt("Курс (1-6): ", 1, 6);
        Faculty faculty = ReadEnum<Faculty>("Факультет:");

        var student = new Student(name, surname, age, email, year, faculty);
        students.Add(student);
        Console.WriteLine($"\nСтудент {student} добавлен.");
    }

    static void AddTeacher()
    {
        Console.WriteLine("Добавление преподавателя");
        string name = ReadString("Имя: ");
        string surname = ReadString("Фамилия: ");
        int age = ReadInt("Возраст: ", 20, 100);
        string email = ReadEmail("Email: ");
        string dept = ReadString("Кафедра: ");
        AcademicDegree degree = ReadEnum<AcademicDegree>("Ученая степень:");

        var teacher = new Teacher(name, surname, age, email, dept, degree);
        teachers.Add(teacher);
        Console.WriteLine($"\nПреподаватель {teacher} добавлен.");
    }

    static void AddCourse()
    {
        Console.WriteLine("Создание курса");
        string name = ReadString("Название: ");
        string desc = ReadString("Описание: ");
        int max = ReadInt("Макс. студентов (1-200): ", 1, 200);

        var course = new Course(name, desc, max);
        courses.Add(course);
        Console.WriteLine($"\nКурс \"{course.Name}\" создан (ID: {course.Id}).");
    }

    static void EnrollStudent()
    {
        Console.WriteLine("Запись студента на курс");

        if (students.Count == 0) { Console.WriteLine("Нет студентов."); return; }
        if (courses.Count == 0) { Console.WriteLine("Нет курсов."); return; }

        Console.WriteLine("Студенты:");
        foreach (var s in students)
            Console.WriteLine($"  {s}");

        int sid = ReadInt("\nID студента: ", 1, int.MaxValue);
        var student = students.FirstOrDefault(s => s.Id == sid);
        if (student == null) { Console.WriteLine("Студент не найден!"); return; }

        Console.WriteLine("\nКурсы:");
        foreach (var c in courses)
            Console.WriteLine($"  ID:{c.Id} - {c.Name} ({c.Students.Count}/{c.MaxStudents})");

        int cid = ReadInt("\nID курса: ", 1, int.MaxValue);
        var course = courses.FirstOrDefault(c => c.Id == cid);
        if (course == null) { Console.WriteLine("Курс не найден!"); return; }

        if (course.Students.Count >= course.MaxStudents)
        {
            Console.WriteLine("Курс заполнен!");
            return;
        }
        if (course.Students.Any(s => s.Id == student.Id))
        {
            Console.WriteLine("Студент уже записан на этот курс!");
            return;
        }

        course.Students.Add(student);
        student.Courses.Add(course);
        Console.WriteLine($"\n{student} записан на курс \"{course.Name}\".");
    }

    static void AssignTeacher()
    {
        Console.WriteLine("Назначение преподавателя на курс");

        if (teachers.Count == 0) { Console.WriteLine("Нет преподавателей."); return; }
        if (courses.Count == 0) { Console.WriteLine("Нет курсов."); return; }

        Console.WriteLine("Преподаватели:");
        foreach (var t in teachers)
            Console.WriteLine($"  {t}");

        int tid = ReadInt("\nID преподавателя: ", 1, int.MaxValue);
        var teacher = teachers.FirstOrDefault(t => t.Id == tid);
        if (teacher == null) { Console.WriteLine("Преподаватель не найден!"); return; }

        Console.WriteLine("\nКурсы:");
        foreach (var c in courses)
        {
            string cur = c.Teacher != null ? c.Teacher.Surname : "нет";
            Console.WriteLine($"  ID:{c.Id} - {c.Name} (преподаватель: {cur})");
        }

        int cid = ReadInt("\nID курса: ", 1, int.MaxValue);
        var course = courses.FirstOrDefault(c => c.Id == cid);
        if (course == null) { Console.WriteLine("Курс не найден!"); return; }

        course.Teacher = teacher;
        if (!teacher.Courses.Any(c => c.Id == course.Id))
            teacher.Courses.Add(course);

        Console.WriteLine($"\n{teacher} назначен на курс \"{course.Name}\".");
    }


    static void ShowStudent()
    {
        Console.WriteLine("Информация о студенте");
        if (students.Count == 0) { Console.WriteLine("Нет студентов."); return; }

        foreach (var s in students)
            Console.WriteLine($"  {s}");

        int id = ReadInt("\nID студента: ", 1, int.MaxValue);
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null) { Console.WriteLine("Не найден!"); return; }

        Console.WriteLine();

        Person person = student;
        person.PrintInfo();

        if (student.Courses.Count > 0)
        {
            Console.WriteLine("\n  Курсы:");
            foreach (var c in student.Courses)
                Console.WriteLine($"    - {c.Name}");
        }
    }

    static void ShowTeacher()
    {
        Console.WriteLine("Информация о преподавателе");
        if (teachers.Count == 0) { Console.WriteLine("Нет преподавателей."); return; }

        foreach (var t in teachers)
            Console.WriteLine($"  {t}");

        int id = ReadInt("\nID преподавателя: ", 1, int.MaxValue);
        var teacher = teachers.FirstOrDefault(t => t.Id == id);
        if (teacher == null) { Console.WriteLine("Не найден!"); return; }

        Console.WriteLine();
        Person person = teacher;
        person.PrintInfo();

        if (teacher.Courses.Count > 0)
        {
            Console.WriteLine("\n  Курсы:");
            foreach (var c in teacher.Courses)
                Console.WriteLine($"    - {c.Name} ({c.Students.Count} студ.)");
        }
    }

    static void ShowCourse()
    {
        Console.WriteLine("Информация о курсе");
        if (courses.Count == 0) { Console.WriteLine("Нет курсов."); return; }

        foreach (var c in courses)
            Console.WriteLine($"  ID:{c.Id} - {c.Name}");

        int id = ReadInt("\nID курса: ", 1, int.MaxValue);
        var course = courses.FirstOrDefault(c => c.Id == id);
        if (course == null) { Console.WriteLine("Не найден!"); return; }

        Console.WriteLine();
        course.PrintInfo();

        if (course.Students.Count > 0)
        {
            Console.WriteLine("\n  Студенты на курсе:");
            foreach (var s in course.Students)
                Console.WriteLine($"    - {s}");
        }
    }

    static void ShowAllStudents()
    {
        Console.WriteLine("Все студенты");
        if (students.Count == 0) { Console.WriteLine("Список пуст."); return; }

        var grouped = students.GroupBy(s => s.Faculty);
        foreach (var group in grouped)
        {
            Console.WriteLine($" [{group.Key}]");
            foreach (var s in group.OrderBy(s => s.Surname))
                Console.WriteLine($"   {s} | курс {s.YearOfStudy} | записан на {s.Courses.Count} курс(ов)");
        }
        Console.WriteLine($"\nВсего: {students.Count}");
    }

    static void ShowAllTeachers()
    {
        Console.WriteLine("Все преподаватели");
        if (teachers.Count == 0) { Console.WriteLine("Список пуст."); return; }

        foreach (var t in teachers.OrderBy(t => t.Surname))
            Console.WriteLine($"  {t} | {t.Department} | курсов: {t.Courses.Count}");

        Console.WriteLine($"\nВсего: {teachers.Count}");
    }

    static void ShowAllCourses()
    {
        Console.WriteLine("Все курсы");
        if (courses.Count == 0) { Console.WriteLine("Список пуст."); return; }

        foreach (var c in courses.OrderBy(c => c.Name))
        {
            string tname = c.Teacher != null ? c.Teacher.Surname : "нет";
            Console.WriteLine($"  ID:{c.Id} | {c.Name} | препод: {tname} | студентов: {c.Students.Count}/{c.MaxStudents}");
        }

        Console.WriteLine($"\nВсего курсов: {courses.Count}");

        if (courses.Count > 0)
        {
            double avg = courses.Average(c => c.Students.Count);
            Console.WriteLine($"Среднее кол-во студентов на курсе: {avg:F1}");
        }
    }
}
