using System;
using System.ComponentModel;

internal class Programm
{
    class WriteToFile
    {
        public StreamWriter sw = new StreamWriter("hreading_log.txt");
        public void Write(string s)
        {
            lock (sw)
            {
                if (Monitor.TryEnter(sw))
                {
                    Console.WriteLine("Поток взял управление файлом");
                    sw.WriteLine(s.ToString());
                }
                else
                {
                    Monitor.Wait(sw);
                }
                if (Monitor.IsEntered(sw))
                {
                    Monitor.Exit(sw);
                    Monitor.PulseAll(sw);
                }             
            }
        }
    }

    class MyThread
    {
        DateTime startTime;
        public int Num { get; private set; }
        public int mls = 0;
        private bool waitNext = false;
        public Thread Thrd;
        public static WriteToFile wf = new WriteToFile();

        // Сконструировать новый поток,
        public MyThread(string name, int num)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Num = num;
            startTime = DateTime.Now;
            Thrd.Start(); // начать поток
            Console.WriteLine("Имя: " + Thrd.Name + " запущен");
            wf.Write("Имя: " + Thrd.Name + " запущен");
        }
        // Начать выполнение нового потока.
        public void Run()
        {
            do
            {
                if (!Thrd.IsAlive)
                {
                    break;
                }
                mls = (int)(Math.Round((DateTime.Now - startTime).TotalMilliseconds));
                if (mls > 0)
                {
                    if (waitNext && mls % 700 != 0 && mls % 900 != 0)
                    {
                        waitNext = false;
                    }

                    if (!waitNext && Num % 2 == 0 && mls % 700 == 0)
                    {
                        Console.WriteLine("Имя: " + Thrd.Name + " Время жизни:" + mls);
                        wf.Write("Имя: " + this.Thrd.Name +
                                 ", приоритет: " + this.Thrd.Priority +
                                 ", хэш-код: " + this.Thrd.ExecutionContext.GetHashCode() +
                                 ", время жизни: " + mls);
                        waitNext = true;
                    }

                    if (!waitNext && Num % 2 != 0 && mls % 900 == 0)
                    {
                        Console.WriteLine("Имя: " + Thrd.Name + " Время жизни:" + mls);
                        wf.Write("Имя: " + this.Thrd.Name +
                                 ", приоритет: " + this.Thrd.Priority +
                                 ", хэш-код: " + this.Thrd.ExecutionContext.GetHashCode() +
                                 ", время жизни: " + mls);
                        waitNext = true;
                    }
                }
            } while(mls <= 10000);
            wf.Write("Имя: " + this.Thrd.Name + " завершен");
            Console.WriteLine("Имя: " + this.Thrd.Name + " завершен");
            return;
        }
    }

        public static void Main(string[] args)
        {
            Console.WriteLine("Основной поток Main запущен");

            MyThread[] myThreads = { new MyThread("Дочерний поток №1", 1), 
                                     new MyThread("Дочерний поток №2", 2), 
                                     new MyThread("Дочерний поток №3", 3), 
                                     new MyThread("Дочерний поток №4", 4), 
                                     new MyThread("Дочерний поток №5", 5), 
                                     new MyThread("Дочерний поток №6", 6) };
            
            foreach (var thread in myThreads)
            {
                thread.Thrd.Join();
            }

            MyThread.wf.sw.Close();
            Console.WriteLine("Основной поток Main завершен.");
    }
}
