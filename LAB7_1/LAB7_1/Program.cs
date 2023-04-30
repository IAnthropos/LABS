using System;

namespace MyDelegates
{
    class DelegateMath
    {
        public delegate Double DelAbs(Double x);
        public delegate Double DelSqrt(Double x);
        public delegate Double DelLogN(Double x, Double n);
        public delegate Double DelPow(Double x, Double n);
        public DelAbs DA;
        public DelSqrt DS;
        public DelLogN DL;
        public DelPow DP;


        public DelegateMath()
        {
            DA = Math.Abs;
            DS = Math.Sqrt;
            DL = Math.Log;
            DP = Math.Pow;
        }

        public double CalulateY(double x)
        {
           return (1 - DS(DA(DL(x, 2)) + 25 * DP(x, (1.0/5.0))) * DL(DP(x,2), 10)) 
                / (DL(x, 2) + 0.00025*DL(x, 10));
        }
    }

    class GroupDelegets
    {
        public static void Pow(double a, ref double x) 
        { 
            x = Math.Pow(x, a);
        }

        public static void Division(double a, ref double x)
        {
            x = a / x;
        }

        public static void Addition(double a, ref double x)
        {
            x += a;
        }

        public static void Multipy(double a, ref double x)
        {
            x *= a;
        }

        public static void Subtraction(double a, ref double x)
        {
            x -= a;
        }

        public void PowNotStatic(double a, ref double x)
        {
            x = Math.Pow(x, a);
        }

        public void DivisionNotStatic(double a, ref double x)
        {
            x = a / x;
        }

        public void AdditionNotStatic(double a, ref double x)
        {
            x += a;
        }

        public void MultipyNotStatic(double a, ref double x)
        {
            x *= a;
        }

        public void SubtractionNotStatic(double a, ref double x)
        {
            x -= a;
        }

        public double CalculateY(double a, double x)
        {
            return Math.Pow(((a / Math.Pow(x, a) + a) * a - a), a);
        }
    }

    internal class Programm
    {
        public delegate void DoubleMod(double a, ref double x);

        public static void Main(string[] args)
        {
            DelegateMath delegateMath = new DelegateMath();
            Console.WriteLine(delegateMath.DA(-1));
            Console.WriteLine(delegateMath.DS(9));
            Console.WriteLine(delegateMath.DL(8, 2));
            Console.WriteLine(delegateMath.DP(8, 2));
            Console.WriteLine(delegateMath.CalulateY(2.0));

            GroupDelegets groupDelegets = new GroupDelegets();

            TextReader oldIn = Console.In;
            bool success;
            success = double.TryParse(oldIn.ReadLine(), out var x);
            if (!success) 
            {
                x = 1;
            }
            success = double.TryParse(oldIn.ReadLine(), out var y);
            if (!success)
            {
                y = 3;
            }

            Console.WriteLine(groupDelegets.CalculateY(y, x));
            var xCopy = x;

            // Делегаты со статичными методами
            DoubleMod groupDelegate = GroupDelegets.Pow;
            groupDelegate += GroupDelegets.Division;
            groupDelegate += GroupDelegets.Addition;
            groupDelegate += GroupDelegets.Multipy;
            groupDelegate += GroupDelegets.Subtraction;
            groupDelegate += GroupDelegets.Pow;

            groupDelegate(y, ref x);
            Console.WriteLine(x);

            // Делегаты с нестатичными методами
            DoubleMod groupDelegateNotStatic = groupDelegets.PowNotStatic;
            groupDelegateNotStatic += groupDelegets.DivisionNotStatic;
            groupDelegateNotStatic += groupDelegets.AdditionNotStatic;
            groupDelegateNotStatic += groupDelegets.MultipyNotStatic;
            groupDelegateNotStatic += groupDelegets.SubtractionNotStatic;
            groupDelegateNotStatic += groupDelegets.PowNotStatic;
            
            groupDelegateNotStatic(y, ref xCopy);
            Console.WriteLine(x);
        }
    }
}
