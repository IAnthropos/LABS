using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

internal class Program
{
    public static ArrayList arrayList;
    public static Queue queue;
    public static Hashtable hashtable;

    private class Student
    {
        private string lastName;
        private string firstName;
        public int course;
        private Hashtable grades;

        public Student(string lastName, string firstName, int course, Hashtable grades)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.course = course;
            this.grades = grades;
        }

        public override string ToString()
        {
            return $"Студент: {lastName} {firstName}, курс {course}";
        }

        public double AverageGrade()
        {
            double summ = 0;
            foreach (int grade in grades.Values)
            {
                summ += grade;
            }
            return summ / grades.Count;
        }
    }

    class MyComparer : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            try
            {
                return (new CaseInsensitiveComparer()).Compare(x, y);
            }
            catch (System.ArgumentException) 
            { 
                return 0; 
            }
        }
    }

    public static void Main()
    {
        // Блок по ArrayList
        Console.WriteLine("ArrayList");
        arrayList = new ArrayList();
        Console.WriteLine(arrayList.Add(1.5));
        arrayList.AddRange(new ArrayList() { 5.1, 2.2, 5.5, 10.0 });
        try
        {
            arrayList.Insert(4, 9.0);
        }
        catch (System.ArgumentOutOfRangeException exception)
        {
            Console.WriteLine(exception.Message);
        }
        try
        {
            IComparer myComparer = new MyComparer();
            arrayList.Sort(myComparer);
        }
        catch (System.InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }
        arrayList.Remove(1.5);
        LowAverageToZero(ref arrayList);
        foreach (object obj in arrayList)
        {
            Console.WriteLine(obj.ToString());
        }

        Console.WriteLine();
        // Блок по Queue 
        Console.WriteLine("Queue");
        queue = new Queue();
        queue.Enqueue(15.1);
        queue.Enqueue(20.1);
        Console.WriteLine(queue.Peek());
        Console.WriteLine(queue.Dequeue());
        Console.WriteLine(queue.Peek());
        Console.WriteLine(queue.Contains(20.1));

        Console.WriteLine();
        // Блок по Hashtable 
        Console.WriteLine("Hashtable");
        hashtable = new Hashtable();
        hashtable.Add(1, 2.4);
        hashtable.Add(2, 2.8);
        hashtable[3] = 5.5;
        Console.WriteLine(hashtable.ContainsKey(1));
        hashtable.Remove(1);
        foreach (var key in hashtable.Keys)
        {
            Console.WriteLine($"Ключ: {key}, значение: {hashtable[key]}");
        }

        // Программа определяет, является ли введенная скобочная структура правильной
        Console.WriteLine(CorrectBracketStructure("()"));
        Console.WriteLine(CorrectBracketStructure("(())()"));
        Console.WriteLine(CorrectBracketStructure("()()"));
        Console.WriteLine(CorrectBracketStructure("((()))"));
        Console.WriteLine(CorrectBracketStructure(")("));
        Console.WriteLine(CorrectBracketStructure("())(()"));
        Console.WriteLine(CorrectBracketStructure("("));
        Console.WriteLine(CorrectBracketStructure("))))"));
        Console.WriteLine(CorrectBracketStructure("((())"));

        // Программа определяет студента с худшей успеваемостью
        var student1 = new Student("Иванов", "Иван", 2, new Hashtable() {{"Математика", 5}, { "Физика", 5 }, { "Химия", 5 } });
        var student2 = new Student("Петров", "Петр", 2, new Hashtable() { { "Математика", 4 }, { "Физика", 4 }, { "Химия", 5 } });
        var student3 = new Student("Сидоров", "Семен", 1, new Hashtable() { { "Математика", 4 }, { "Физика", 5 }, { "Химия", 5 } });
        var student4 = new Student("Смирнов", "Олег", 3, new Hashtable() { { "Математика", 4 }, { "Физика", 4 }, { "Химия", 4 } });
        var student5 = new Student("Кузнецов", "Алексей", 2, new Hashtable() { { "Математика", 3 }, { "Физика", 4 }, { "Химия", 4 } });
        var student6 = new Student("Мельников", "Александр", 1, new Hashtable() { { "Математика", 3 }, { "Физика", 3 }, { "Химия", 3 } });

        Student studentWithWorstGrades = null;
        // Решение с массивом 
        Student[] students= new Student[] { student1, student2, student3, student4, student5, student6};
        foreach(var student in students)
        {
            if (studentWithWorstGrades is null && student.course == 2)
            {
                studentWithWorstGrades = student;
            }
            else if (student.course == 2 && student.AverageGrade() < studentWithWorstGrades.AverageGrade())
            {
                studentWithWorstGrades = student;
            }
        }

        if (studentWithWorstGrades is not null)
        {
            Console.WriteLine(studentWithWorstGrades);
            Console.WriteLine($"Средний бал: {studentWithWorstGrades.AverageGrade().ToString("F2")}");
        }
        // Решение с очередью
        Queue<Student> queueStudents = new Queue<Student>(students); 
        while (queueStudents.Count > 0) 
        { 
            var student = queueStudents.Dequeue();
            if (studentWithWorstGrades is null && student.course == 2)
            {
                studentWithWorstGrades = student;
            }
            else if (student.course == 2 && student.AverageGrade() < studentWithWorstGrades.AverageGrade())
            {
                studentWithWorstGrades = student;
            }
        }
        if (studentWithWorstGrades is not null)
        {
            Console.WriteLine(studentWithWorstGrades);
            Console.WriteLine($"Средний бал: {studentWithWorstGrades.AverageGrade().ToString("F2")}");
        }
    }

    public static void LowAverageToZero(ref ArrayList arrayList)
    {
        double val = 0;
        foreach(var item in arrayList) 
        {
            val += (double)item;
        }
        val /= arrayList.Count;
        for (int i = 0; i < arrayList.Count; i++) 
        { 
            if ((double)arrayList[i] < val)
            {
                arrayList[i] = 0.0;
            }
        }
    }

    public static bool CorrectBracketStructure(string bracketStructure)
    {
        var result = true;
        var closeBracketCount = 0;
        var stack = new Stack();
        foreach (var letter in bracketStructure) 
        {
            if (letter == '(' || letter == ')')
            {
                stack.Push(letter);
            }
        }
        while (stack.Count > 0) 
        { 
            var letter = (char)stack.Pop();
            if (letter == ')')
            {
                closeBracketCount++;
            }
            else 
            { 
                if (closeBracketCount == 0)
                {
                    return false;
                }
                else
                {
                    closeBracketCount--;
                }
            }
        }

        return closeBracketCount == 0;
    }

}