using System;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

class SizeArrayClass
{
    public static readonly int SIZE = 100;

}

internal class Program
{
    class Array
    {
        int[] massive = new int[SizeArrayClass.SIZE]; 

        public Array(int[] massive)
        {
            for (int i = 0; i < massive.Length; i++) 
            {
                this.massive[i] = massive[i];
            }
        }
        public int[] SortReverse()
        {
            lock (massive)
            {
                System.Array.Sort(massive);
                System.Array.Reverse(massive);
                return massive;
            }
        }
    }
    
    public static void Main(string[] args)
    {
        int[] massive = new int[SizeArrayClass.SIZE];

        Random random = new Random();
        unsafe
        {
            fixed (int* p = massive)
            {
                for (int i = 0; i < SizeArrayClass.SIZE; i++)
                    *(p + i) = (int)Math.Pow(random.Next(0,201), 2); 
            }
            
            // Размер массива
            Console.WriteLine(massive.Length * sizeof(int));

            // Введите число R большн которого элементы, не будут сохранены в unsafe.txt 
            Console.WriteLine(massive.Length * sizeof(int));
            int R = 0;
            try
            {
                checked
                {
                    R = int.Parse(Console.ReadLine());
                }
            }
            catch (OverflowException) 
            {
                Console.WriteLine("Переполнение");
            }

            Array array = new Array(massive);
            var sortedReverseMassive = array.SortReverse();
            for (int i = 0; i < SizeArrayClass.SIZE; i++)
            {
                Console.WriteLine(sortedReverseMassive[i]);
            }
            string path = "unsafe.txt";
            var selectedNums = from num in massive 
                               where num <= R
                               orderby num descending
                               select num;   
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var num in selectedNums)
                {
                    writer.WriteLine(num);
                }
            }
        }
        
    }
}
