using Indexer;
using System;

namespace Indexer
{
    internal class RandomArray
    {
        private int[] a; // ссылка на базовый массив

        public int Length { get; private set; } // наибольший индекс
        public bool Error { get; private set; }

        public RandomArray(int length)
        {
            Length = length;
            Error = false;
            a = new int[Length];
            Init();
        }

        public int this[int index]
        {
            // Это аксессор get.
            get
            {
                if (InBound(index))
                {
                    Error = false;
                    return a[index];
                }
                Error = true;
                Console.WriteLine("Индекс вне границ массива");
                return 0;
            }
            // Это аксессор set.
            set
            {
                if (InBound(index))
                {
                    if (PowerOfTwo(value))
                    {
                        a[index] = value;
                        Error = false;
                    }
                    else 
                    {
                        Error = true;
                        Console.WriteLine("Значение не степень 2-ки");
                    }
                }
                Error = true;
                Console.WriteLine("Индекс вне границ массива");
            }
        }

        private void Init()
        {
            Console.WriteLine("Сколько элементов массива нужно внести руками:");
            var line = Console.ReadLine();


            var success = int.TryParse(line, out var num);
            if (success) 
            {
                var counter = 0;

                while (counter < num)
                {
                    Console.WriteLine("Введите число степень 2-ки");
                    var newElem = Console.ReadLine();

                    if (int.TryParse(newElem, out var elem))
                    {
                        a[counter++] = elem;
                    }
                    else
                    {
                        Console.WriteLine("Попробуйте еще раз");
                    }
                }

                for (int i = num; i < Length; i++)
                {
                    a[i] = (int)Math.Pow(2, i);
                }
            }
            else
            {
                Console.WriteLine("Введено не число, массив будет заполнен случайными числами");
            }

 
        }

        public void Print()
        {
            Console.WriteLine("Все элементы массива:");
            for (int i = 0; i < Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }

        public int this[double indx]
        {
            // Это аксессор get.
            get
            {
                var index = (int)indx;

                if (InBound(index))
                {
                    Error = false;
                    return a[index];
                }
                Error = true;
                Console.WriteLine("Индекс вне границ массива");
                return 0;
            }
            // Это аксессор set.
            set
            {
                var index = (int)indx;

                if (InBound(index))
                {
                    if (PowerOfTwo(value))
                    {
                        a[index] = value;
                        Error = false;
                    }
                    else
                    {
                        Error = true;
                        Console.WriteLine("Значение не степень 2-ки");
                    }
                }
                else
                {
                    Error = true;
                    Console.WriteLine("Индекс вне границ массива");
                }
            }
        }

        private bool InBound(int index)
        {
            return index >= 0 && index < Length;
        }
            
        private bool PowerOfTwo(int value)
        {
            int initValue = 1;
            while (initValue < value)
            {
                initValue *= 2;
            }

            if (initValue == value)
            {
                return true;
            }
            return false;
        }

        public double AmountOfDegrees()
        {
            double result = 1;
            int summ = 0;

            for (int i = 0; i < Length; i++)
            {
                var tmp = a[i];
                var count = 0;
                while (tmp != 1)
                {
                    tmp = tmp / 2;
                    count++;
                }
                result *= count;
                summ += count;
            }
            return result / summ;
        }
    }
    
    class NaturalNum
    {
        private double degree;
        public int Base { get; set; }
        public double Degree 
        { 
            get { return Math.Pow(Base, degree); }
            set { degree = value; } 
        }
    }

    public class Program
    {
        public static void Main(string[] args) 
        {
            var randomArray = new RandomArray(3);
            randomArray.Print();
            Console.WriteLine($"Значение по индексу a[1]: {randomArray[1.1]}");
            var num = randomArray[11];
            randomArray[1.1] = 7;
            randomArray[0] = 16;
            Console.WriteLine($"Значение по индексу a[0]: {randomArray[0.1]}");
            randomArray.Print();
            Console.WriteLine("Отношение AmountOfDegrees:");
            Console.WriteLine(randomArray.AmountOfDegrees().ToString("F2"));

            NaturalNum naturalNum = new NaturalNum();
            naturalNum.Base= 3;
            naturalNum.Degree = 2;
            Console.WriteLine(naturalNum.Degree);
        }
    }
}
