using System;

namespace LABS1_2
{
    /*
     * Вариант № 4
     */
    internal class Program
    {
        public static void Main(string[] args)
        {
            var triangle = new Triangle(10, 8, 0.89605203797);
            Console.WriteLine($"a = {triangle.La}, b = {triangle.Lb}, c = {triangle.Lc}");
            Console.WriteLine($"<a = {triangle.AngleA}, <b = {triangle.AngleB}, <c = {triangle.AngleC}");
            Console.WriteLine($"Check sum of angles: {triangle.AngleA + triangle.AngleB + triangle.AngleC}");
            Console.WriteLine($"S = {triangle.CalculateS()}");
        }
    }
}