using System;

namespace LAB1_1
{
    internal class Program
    {
        /*
         * Вариант № 1
         */
        public static void Main(string[] args)
        {
            var xs = new double[] { -5, -2, 2, 5 };
            foreach (var x in xs)
            {
                Console.WriteLine($"Y = {CalculateY(x)}");
                Console.WriteLine($"Y = {CalculateYSimplifiedFormula(x)}");
            }
            Console.WriteLine("Введите значение x: ");
            var val = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Y = {CalculateY(val)}");
            Console.WriteLine($"Y = {CalculateYSimplifiedFormula(val)}");
        }

        private static double CalculateY(double x)
        {
            var numerator = 2 * Math.Pow(x, 3) + 6 * Math.Pow(x, 2) - 8 * x + 4;
            var denominator = -4 * Math.Pow(x, 3) + 8 * Math.Pow(x, 2) - Math.Pow(x, 5) + 2 * Math.Pow(x, 4);
            return numerator / denominator;
        }
        private static double CalculateYSimplifiedFormula(double x)
        {
            var numerator = 2 * Math.Pow(x, 2) * (x + 3) - 4 * (2 * x - 1);
            var denominator = -Math.Pow(x, 2) * (4 + Math.Pow(x, 2)) * (x - 2);
            return numerator / denominator;
        }
    }
}