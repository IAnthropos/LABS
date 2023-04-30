using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

class ParallelArray
{
    public static Random random;
    public int[,] numbers;

    public ParallelArray()
    {
        numbers = new int[5, 20];
        random = new Random();

        int rows = numbers.GetUpperBound(0) + 1;    // количество строк
        int columns = numbers.Length / rows;        // количество столбцов

        for (int i = 0; i < rows; i++)
            for (int k = 0; k < columns; k++)
            {
                numbers[i, k] = random.Next(10, 100);
            }
    }

    public void BubbleSort(int num)
    {
        var taskId = num;
        Console.WriteLine($"MyTask({taskId}) запущен");

        if (taskId % 2 == 0)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20 - 1; j++)
                    if (numbers[taskId, j] > numbers[taskId, j + 1])
                    {
                        var t = numbers[taskId, j + 1];
                        numbers[taskId, j + 1] = numbers[taskId, j];
                        numbers[taskId, j] = t;
                    }
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20 - 1; j++)
                    if (numbers[taskId, j] < numbers[taskId, j + 1])
                    {
                        var t = numbers[taskId, j + 1];
                        numbers[taskId, j + 1] = numbers[taskId, j];
                        numbers[taskId, j] = t;
                    }
            }
        }
    }

    public void BubbleSort0()
    {
        BubbleSort(0);
    }

    public void BubbleSort1()
    {
        BubbleSort(1);
    }

    public void BubbleSort2()
    {
        BubbleSort(3);
    }

    public void BubbleSort3()
    {
        BubbleSort(3);
    }

    public void BubbleSort4()
    {
        BubbleSort(4);
    }

    public void Print()
        {
            int rows = numbers.GetUpperBound(0) + 1;    // количество строк
            int columns = numbers.Length / rows;        // количество столбцов

            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    Console.Write(numbers[i, k]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
}

class Array
{
    public static Random random;
    public int[,] numbers;
    // Метод выполняемый в качестве задачи.
    public Array()
    {
        numbers = new int[5, 20];
        random = new Random();

        int rows = numbers.GetUpperBound(0) + 1;    // количество строк
        int columns = numbers.Length / rows;        // количество столбцов

        for (int i = 0; i < rows; i++)
            for (int k = 0; k < columns; k++)
            {
                numbers[i, k] = random.Next(10, 100);
            }
    }
    public void BubbleSort()
    {

        var taskId = (int)Task.CurrentId - 8; 
        Console.WriteLine($"MyTask({taskId}) запущен");

        if (taskId % 2 == 0)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20 - 1; j++)
                    if (numbers[taskId, j] > numbers[taskId, j + 1])
                    {
                        var t = numbers[taskId, j + 1];
                        numbers[taskId, j + 1] = numbers[taskId, j];
                        numbers[taskId, j] = t;
                    }
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20 - 1; j++)
                    if (numbers[taskId, j] < numbers[taskId, j + 1])
                    {
                        var t = numbers[taskId, j + 1];
                        numbers[taskId, j + 1] = numbers[taskId, j];
                        numbers[taskId, j] = t;
                    }
            }
        }

        Console.WriteLine($"MyTask({taskId}) завершен ");
    }

    public int FirstStep()
    {
        return 0;
    }

    public int ContTaskMethod(Task<int> t)
    {
        var taskId = t.Result;
        Console.WriteLine($"MyTask({taskId}) запущен");

        if (taskId % 2 == 0)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20 - 1; j++)
                    if (numbers[taskId, j] > numbers[taskId, j + 1])
                    {
                        var temp = numbers[taskId, j + 1];
                        numbers[taskId, j + 1] = numbers[taskId, j];
                        numbers[taskId, j] = temp;
                    }
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20 - 1; j++)
                    if (numbers[taskId, j] < numbers[taskId, j + 1])
                    {
                        var temp = numbers[taskId, j + 1];
                        numbers[taskId, j + 1] = numbers[taskId, j];
                        numbers[taskId, j] = temp;
                    }
            }
        }

        Console.WriteLine($"MyTask({taskId}) завершен ");
        
        taskId++;
        return taskId;
    }

    public void Print() 
    {
        int rows = numbers.GetUpperBound(0) + 1;    // количество строк
        int columns = numbers.Length / rows;        // количество столбцов

        for (int i = 0; i < rows; i++)
        {
            for (int k = 0; k < columns; k++)
            {
                Console.Write(numbers[i, k]);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}

internal class Program
{
    public static void Main(string[] args)
    {
        Array array = new Array();

        // первый массив задач
        Task[] tasks = new Task[5];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Factory.StartNew(array.BubbleSort);
        }

        Task.WaitAll(tasks);
        array.Print();

        Array array2 = new Array();
        var numbers = array2.numbers;

        // второй массив задач
        Task[] tasks2 = new Task[5];
        for (int i = 0; i < tasks2.Length; i++)
        {
            var taskId = i;
            tasks2[i] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"MyTask({taskId}) запущен");

                if (taskId % 2 == 0)
                {
                    for (int l = 0; l < 20; l++)
                    {
                        for (int j = 0; j < 20 - 1; j++)
                        {
                            if (numbers[taskId, j] > numbers[taskId, j + 1])
                            {
                                var t = numbers[taskId, j + 1];
                                numbers[taskId, j + 1] = numbers[taskId, j];
                                numbers[taskId, j] = t;
                            }
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < 20; l++)
                    {
                        for (int j = 0; j < 20 - 1; j++)
                        {
                            if (numbers[taskId, j] < numbers[taskId, j + 1])
                            {
                                var t = numbers[taskId, j + 1];
                                numbers[taskId, j + 1] = numbers[taskId, j];
                                numbers[taskId, j] = t;
                            }
                        }
                    }
                }

                Console.WriteLine($"MyTask({taskId}) завершен ");
            });
        }

        Task.WaitAll(tasks2);
        array2.Print();


        Array array3 = new Array();
        Task<int> tsk = new Task<int>(array3.FirstStep);
        Task<int> tskCont  = tsk.ContinueWith<int>(array3.ContTaskMethod);
        Task<int> tskCont1 = tskCont.ContinueWith<int>(array3.ContTaskMethod);
        Task<int> tskCont2 = tskCont1.ContinueWith<int>(array3.ContTaskMethod);
        Task<int> tskCont3 = tskCont2.ContinueWith<int>(array3.ContTaskMethod);
        Task<int> tskCont4 = tskCont3.ContinueWith<int>(array3.ContTaskMethod);
        tsk.Start();
        Task.WaitAll(tskCont4);
        array3.Print();

        ParallelArray parallelArray  = new ParallelArray();
        Parallel.For(0, 5, parallelArray.BubbleSort);
        parallelArray.Print();

        ParallelArray parallelArray1 = new ParallelArray();
        ParallelLoopResult loopResult = Parallel.ForEach(new List<int>() { 0, 1, 2, 3, 4 }, parallelArray1.BubbleSort);
        parallelArray1.Print();

        ParallelArray parallelArray2 = new ParallelArray();
        Parallel.Invoke(parallelArray2.BubbleSort0, 
                        parallelArray2.BubbleSort1, 
                        parallelArray2.BubbleSort2, 
                        parallelArray2.BubbleSort3,
                        parallelArray2.BubbleSort4);
        parallelArray2.Print();
    }
}
