using System;
using System.Numerics;

namespace MyDelegates
{
    class GroupDelegets
    {
        public delegate void DoubleMod(double a, ref double x);
        
        public static DoubleMod allOperations;

        public static DoubleMod Pow = delegate(double a, ref double x)
        {
            x = Math.Pow(x, a);
        };
        public static DoubleMod Division = delegate(double a, ref double x)
        {
            x = a / x;
        };
        public static DoubleMod Addition = delegate(double a, ref double x)
        {
            x += a;
        };

        public static DoubleMod Multipy = delegate (double a, ref double x)
        {
            x *= a;
        };

        public static DoubleMod Subtraction = delegate (double a, ref double x)
        {
            x -= a;
        };

        public double CalculateY(double a, double x)
        {
            return Math.Pow(((a / Math.Pow(x, a) + a) * a - a), a);
        }
    }

    static class Calculator
    {
        public delegate BigInteger delBigInteger(int n);
        public delegate double delDouble(double n);
        public delegate decimal delDecimal(decimal n);
        public delegate bool delBool(int n);

        //Возврат флага для проверки, что число - степень двойки 
        public static delBool PowOfTwo = n => n > 0 && (n & -n) == n;

        //Вычисление факториала аргумента 
        public static delBigInteger Fact = n =>
        {
            var factorial = new BigInteger(1);
            for (int i = 1; i <= n; i++)
                factorial *= i;

            return factorial;
        };

        //Вычисление обратного числового значения аргумента
        public static delDouble Reciprocal = n => 1.0 / n;

        //Возврат дробной части числового аргумента
        public static delDecimal FracPart = n =>  n % 1;
        
        //Возврат флага четности аргумента
        public static delBool IsEven = n => n % 2 == 0;

        //Возврат флага нечетности аргумента
        public static delBool IsOdd = n => !IsEven(n);
        

        //Вычисление кубического корня аргумента
        public static delDouble Crt = n => Math.Pow(n, (1.0 / 3.0));

        //Вычисление радиан по аргументу в градусах
        public static delDouble DegToRad = n => n * Math.PI / 180;

        //Вычисление градусов по аргументу в радианах
        public static delDouble RadToDeg = n => n * 180 / Math.PI;
    }

        internal class Programm
    {
        public static void Main(string[] args)
        {
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

            // Делегаты со статичными методами
            GroupDelegets.allOperations = GroupDelegets.Pow;
            GroupDelegets.allOperations += GroupDelegets.Division;
            GroupDelegets.allOperations += GroupDelegets.Addition;
            GroupDelegets.allOperations += GroupDelegets.Multipy;
            GroupDelegets.allOperations += GroupDelegets.Subtraction;
            GroupDelegets.allOperations += GroupDelegets.Pow;

            GroupDelegets.allOperations(y, ref x);
            Console.WriteLine(x);

            // Проверка калькулятора
            Console.WriteLine(Calculator.Fact(10));
            Console.WriteLine(Calculator.Reciprocal(5));
            Console.WriteLine(Calculator.FracPart(10.5443m));
            Console.WriteLine(Calculator.IsEven(5642));
            Console.WriteLine(Calculator.IsOdd(567));
            Console.WriteLine(Calculator.Crt(27));
            Console.WriteLine(Calculator.DegToRad(30));
            Console.WriteLine(Calculator.RadToDeg(Calculator.DegToRad(30)).ToString("F2"));
            Console.WriteLine(Calculator.PowOfTwo(1024));
            Console.WriteLine(Calculator.PowOfTwo(1025));
        }
    }
}