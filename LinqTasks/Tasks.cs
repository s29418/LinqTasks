using LinqTasks.Extensions;
using LinqTasks.Models;

namespace LinqTasks;

public static partial class Tasks
{
    public static IEnumerable<Emp> Emps { get; set; }
    public static IEnumerable<Dept> Depts { get; set; }

    static Tasks()
    {
        Depts = LoadDepts();
        Emps = LoadEmps();
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
    /// </summary>
    public static IEnumerable<Emp> Task1()
    {
        return Emps.Where(emp => emp.Job.Equals("Backend programmer"));
    }

    /// <summary>
    ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
    /// </summary>
    public static IEnumerable<Emp> Task2()
    {
        return Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
            .OrderByDescending(emp => emp.Ename);
    }


    /// <summary>
    ///     SELECT MAX(Salary) FROM Emps;
    /// </summary>
    public static int Task3()
    {
        return Emps.Max(emp => emp.Salary);
    }

    /// <summary>
    ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
    /// </summary>
    public static IEnumerable<Emp> Task4()
    {
        int maxSalary = Task3();
        return Emps.Where(emp => emp.Salary == maxSalary);
    }

    /// <summary>
    ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
    /// </summary>
    public static IEnumerable<object> Task5()
    {
        return Emps.Select(emp => new 
            { 
                Nazwisko = emp.Ename, 
                Praca = emp.Job 
            });
    }

    /// <summary>
    ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
    ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
    ///     Rezultat: Złączenie kolekcji Emps i Depts.
    /// </summary>
    public static IEnumerable<object> Task6()
    {
        return Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
        {
            emp.Ename, 
            emp.Job, 
            dept.Dname
        });
    }

    /// <summary>
    ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
    /// </summary>
    public static IEnumerable<object> Task7()
    {
        return Emps.GroupBy(emp => emp.Job)
            .Select(group => new
            {
                Praca = group.Key, 
                LiczbaPracownikow = group.Count()
            });
    }

    /// <summary>
    ///     Zwróć wartość "true" jeśli choć jeden
    ///     z elementów kolekcji pracuje jako "Backend programmer".
    /// </summary>
    public static bool Task8()
    { 
        return Emps.Any(emp => emp.Job == "Backend programmer");
    }

    /// <summary>
    ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
    ///     ORDER BY HireDate DESC;
    /// </summary>
    public static Emp Task9()
    {
        return Emps.Where(emp => emp.Job == "Frontend programmer")
            .OrderByDescending(emp => emp.HireDate)
            .FirstOrDefault();
    }

    /// <summary>
    ///     SELECT Ename, Job, Hiredate FROM Emps
    ///     UNION
    ///     SELECT "Brak wartości", null, null;
    /// </summary>
    public static IEnumerable<object> Task10()
    {
        var emps = Emps.Select(emp => new
        {
            Ename = emp.Ename,
            Job = emp.Job,
            Hiredate = emp.HireDate
        });

        var additionalObject = new[] { new
        {
            Ename = "Brak wartości", 
            Job = (string)null, 
            Hiredate = (DateTime?)null
        } };

        return emps.Concat(additionalObject);
    }

    /// <summary>
    ///     Wykorzystując LINQ pobierz pracowników podzielony na departamenty pamiętając, że:
    ///     1. Interesują nas tylko departamenty z liczbą pracowników powyżej 1
    ///     2. Chcemy zwrócić listę obiektów o następującej srukturze:
    ///     [
    ///     {name: "RESEARCH", numOfEmployees: 3},
    ///     {name: "SALES", numOfEmployees: 5},
    ///     ...
    ///     ]
    ///     3. Wykorzystaj typy anonimowe
    /// </summary>
    public static IEnumerable<object> Task11()
    {
        return Emps.GroupBy(emp => emp.Deptno)
            .Where(group => group.Count() > 1)
            .Select(group => new 
            {
                name = Depts.First(dept => dept.Deptno == group.Key).Dname,
                numOfEmployees = group.Count()
            });
    }

    /// <summary>
    ///     Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
    ///     Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
    ///     Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
    ///     Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
    /// </summary>
    public static IEnumerable<Emp> Task12()
    {
        IEnumerable<Emp> result = Emps.GetEmpsWithSubordinates();
        
        return result;
    }

    /// <summary>
    ///     Poniższa metoda powinna zwracać pojedyczną liczbę int.
    ///     Na wejściu przyjmujemy listę liczb całkowitych.
    ///     Spróbuj z pomocą LINQ'a odnaleźć tę liczbę, które występuja w tablicy int'ów nieparzystą liczbę razy.
    ///     Zakładamy, że zawsze będzie jedna taka liczba.
    ///     Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
    /// </summary>
    public static int Task13(int[] arr)
    {
        return arr.GroupBy(num => num)
            .First(group => group.Count() % 2 != 0)
            .Key;
    }

    /// <summary>
    ///     Zwróć tylko te departamenty, które mają 5 pracowników lub nie mają pracowników w ogóle.
    ///     Posortuj rezultat po nazwie departament rosnąco.
    /// </summary>
    public static IEnumerable<Dept> Task14()
    {
        return Depts.Where(dept => 
            {
                var count = Emps.Count(emp => emp.Deptno == dept.Deptno);
                return count == 5 || count == 0;
            })
            .OrderBy(dept => dept.Dname);
    }
    
    public static void PrintEmployeeCountPerDept()
    {
        var result = Emps.GroupBy(emp => emp.Deptno)
            .Select(group => new
            {
                DeptName = Depts.FirstOrDefault(dept => dept.Deptno == group.Key)?.Dname,
                EmployeeCount = group.Count()
            });

        foreach (var item in result)
        {
            if (item.DeptName == null)
            {
                Console.WriteLine($"Department: Unknown, Employee Count: {item.EmployeeCount}");
            }
            else
            {
                Console.WriteLine($"Department: {item.DeptName}, Employee Count: {item.EmployeeCount}");
            }
        }
    }
}