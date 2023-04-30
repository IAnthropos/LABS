using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class MonitorTestExample
{
    static Random rnd;
    static int LastThreadId = 0;
    public static double meanForAllThreads = 0.0;

    static void fornewthread()
    {
        int[] values = new int[10000];
        int thrTotal = 0;
        int thrN = 0;
        int ctr = 0;
        Thread.CurrentThread.Name = $"Thread №{++LastThreadId}";
        Monitor.Enter(rnd);
        // Generate 10,000 random integers
        for (ctr = 0; ctr < 10000; ctr++)
            values[ctr] = rnd.Next(0, 1001);
        Monitor.Exit(rnd);
        thrN = ctr;
        foreach (var value in values)
            thrTotal += value;
        double meanForCurrentThread = (thrTotal * 1.0) / thrN;

        Console.WriteLine("Mean for thread {0,2}: {1:N2} (N={2:N0})",
                          Thread.CurrentThread.Name, meanForCurrentThread, thrN);

        if (Monitor.TryEnter(meanForAllThreads))
        {
            meanForAllThreads += meanForCurrentThread;
        }
        else
        {
            Monitor.Wait(meanForAllThreads);
        }
        if (Monitor.IsEntered(meanForAllThreads))
        {
            Monitor.Exit(meanForAllThreads);
            Monitor.PulseAll(meanForAllThreads);
        }
    }
    public static void Main()
    {
        List<Thread> threads = new List<Thread>();
        rnd = new Random();
        int n = 10;

        for (int taskCtr = 0; taskCtr < n; taskCtr++)
            threads.Add(new Thread(fornewthread));
        try
        {
            foreach (Thread t in threads)
            {
                t.Start();
                t.Join();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Exc: " + e.Message);
        }
        Console.WriteLine($"Среднее по всем потокам {(meanForAllThreads / MonitorTestExample.LastThreadId).ToString("F2")}");
        Console.ReadKey();
    }
}