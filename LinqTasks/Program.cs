﻿using LinqTasks.Models;

namespace LinqTasks;

class Program
{
    static void Main(string[] args)
    {
        
        
        Console.WriteLine("======= ZAD 1 =======");
        IEnumerable<Emp> result = Tasks.Task1();
        foreach (Emp emp in result)
        {
            Console.WriteLine(emp);
        }
        
        Console.WriteLine("======= ZAD 2 =======");
        IEnumerable<Emp> result2 = Tasks.Task2();
        foreach (Emp emp in result2)
        {
            Console.WriteLine(emp);
        }
        
        Console.WriteLine("======= ZAD 3 =======");
        int result3 = Tasks.Task3();
        Console.WriteLine(result3);
        
        
        Console.WriteLine("======= ZAD 4 =======");
        IEnumerable<Emp> result4 = Tasks.Task4();
        foreach (Emp emp in result4)
        {
            Console.WriteLine(emp);
        }
        
        Console.WriteLine("======= ZAD 5 =======");
        IEnumerable<object> result5 = Tasks.Task5();
        foreach (object o in result5)
        {
            Console.WriteLine(o);
        }
        
        Console.WriteLine("======= ZAD 6 =======");
        IEnumerable<object> result6 = Tasks.Task6();
        foreach (object o in result6)
        {
            Console.WriteLine(o);
        }
        
        Console.WriteLine("======= ZAD 7 =======");
        IEnumerable<object> result7 = Tasks.Task7();
        foreach (object o in result7)
        {
            Console.WriteLine(o);
        }
        
        
    }
}