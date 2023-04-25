using System;

namespace LABS1_2
{
    public class Triangle
    {
        public double La { get; set; }
        public double Lb { get; set; }
        public double Lc { get; set; }
        public double AngleC { get; set; }
        public double AngleB { get; set; }
        public double AngleA { get; set; }
        public Triangle(double la, double lb, double angleC)
        {
            La = la;
            Lb = lb;
            AngleC = angleC;
            Lc = Math.Sqrt(Math.Pow(La, 2) + Math.Pow(Lb, 2) - 2 * La * Lb * Math.Cos(AngleC));
            AngleA = Math.Asin((Math.Sin(angleC) * La) / Lc);
            AngleB = Math.Asin((Math.Sin(angleC) * Lb) / Lc);
        }

        public double CalculateS()
        {
            return La * Lb * Math.Sin(AngleC) / 2;
        }
    }
}

