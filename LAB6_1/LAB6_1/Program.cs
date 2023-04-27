using System;
using System.IO;
using System.Text;
using System.Collections;

namespace WorkWithFiles
{
    class Program
    {
        static ArrayList _array;
        static int[] _massive;
        static void Main(string[] args)
        {
            StreamWriter log_out;
            try
            {
                log_out = new StreamWriter("system.log");
            }
            catch (IOException exc) 
            {
                Console.WriteLine(exc.Message + " Ошибка открытия файла.");
                return;
            }
            Console.SetOut(log_out);
            Console.WriteLine("Начало системного журнала");
            WriteDataToFile();
            ReadDataFromFile(log_out);
            WriteDataToOtherFile();
            Console.WriteLine("Конец системного журнала");
            log_out.Close();
        }

        static void WriteDataToFile()
        {
            Console.WriteLine("Введите количество строк");

            bool success = int.TryParse(Console.ReadLine(), out var lines);
            if (success)
            {
                Console.WriteLine($"Введено количество строк {lines}");
            }
            else
            {
                lines = 30;
                Console.WriteLine($"Не удалось считать количество строк, взято значение по-умолчанию {lines}");
            }
            Console.WriteLine($"Пользователь ввел {lines} строк");
            FileStream fout = null;
            try
            {
                fout = new FileStream("1.txt", FileMode.OpenOrCreate);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message + " Ошибка открытия файла.");
                Console.ReadKey();
                return;
            }
            Random rnd = new Random();
            for (int i = 0; i < lines; i++)
            {
                var num = rnd.Next(10, 100);
                byte[] buffer = Encoding.Default.GetBytes(num.ToString() + '\n');
                try
                {
                    fout.Write(buffer);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message + " Ошибка при чтении или записи файла. ");
                }
            }
            fout.Close();
            Console.WriteLine($"В файл 1.txt записано {lines} строк");
        }

        static void ReadDataFromFile(StreamWriter log_out)
        {
            FileStream fin = null;
            try
            {
                fin = new FileStream("1.txt", FileMode.Open, FileAccess.Read);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message + " Ошибка открытия файла.");
                Console.ReadKey();
                return;
            }

            int i;
            _array = new ArrayList();
            try
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fin.Read(b, 0, b.Length) > 0)
                {
                    _array.AddRange(temp.GetString(b).Split('\n'));
                }
                fin.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message + " Ошибка при чтении или записи файла. ");
            }
            finally
            {
                fin.Close();
            }

            var OddCount = 0;
            var EvenCount = 0;
            var geometricMean = 1.0;
            var ariphmeticMean = 0.0;

            StreamWriter file_copy;
            try
            {
                file_copy = new StreamWriter("1_copy.txt");
            }
            catch (IOException exc)
            {
                Console.WriteLine(exc.Message + " Ошибка открытия файла.");
                return;
            }
            Console.SetOut(file_copy);

            foreach (var elem in _array)
            {
                Console.WriteLine(elem);
                if (int.TryParse(elem.ToString(), out var num))
                {
                    if (num % 2 == 0)
                    {
                        OddCount++;
                    }
                    else
                    {
                        EvenCount++;
                    }
                    ariphmeticMean += num;
                    geometricMean *= num;
                }
            }
            file_copy.Close();

            Console.SetOut(log_out);

            Console.WriteLine($"Количество четных чисел {OddCount}");
            Console.WriteLine($"Количество нечетных чисел {EvenCount}");
            if (OddCount > EvenCount)
            {
                ariphmeticMean /= (OddCount + EvenCount);
                Console.WriteLine($"Среднее арифметическое {ariphmeticMean}");
            }
            else
            {
                geometricMean = Math.Pow(geometricMean, (1.0 / (OddCount + EvenCount)));
                Console.WriteLine($"Среднее геометрическое {geometricMean}");
            }
            _massive = new int[OddCount + EvenCount];
        }

        static void WriteDataToOtherFile()
        {
            var minIndex = 0;
            var maxIndex = 0;
            for (int i = 0; i < _array.Count; i++)
            {
                if (int.TryParse(_array[i].ToString(), out var num))
                {
                    _massive[i] = num;
                }
            }

            for (int i = 0; i < _massive.Length; i++)
            {
                if (_massive[minIndex] > _massive[i])
                {
                    minIndex = i;
                }
                if (_massive[maxIndex] < _massive[i])
                {
                    maxIndex = i;
                }
            }

            Console.WriteLine($"Максимальное число {_massive[maxIndex]}");
            Console.WriteLine($"Минимальное число {_massive[minIndex]}");
            var start = minIndex;
            var end = maxIndex;
            
            if (minIndex > maxIndex)
            {
                start = maxIndex;
                end = minIndex;
            }

            FileStream fout = null;
            try
            {
                fout = new FileStream("2.txt", FileMode.OpenOrCreate);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message + " Ошибка открытия файла.");
                Console.ReadKey();
                return;
            }

            for (int i = start; i <= end; i++)
            {
                byte[] buffer = Encoding.Default.GetBytes(_array[i].ToString() + '\n');
                try
                {
                    fout.Write(buffer);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message + " Ошибка при чтении или записи файла. ");
                }
            }
            
            fout.Close();
            Console.WriteLine($"В файл 2.txt записано {end - start} строк");
        }
            
    }
}