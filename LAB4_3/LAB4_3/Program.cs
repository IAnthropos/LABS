using System;
using System.Numerics;

namespace LAB4_3
{
    static class Programm
    {
        //Вычисление факториала аргумента 
        public static BigInteger Fact(int n)
        {
            var factorial = new BigInteger(1);
            for (int i = 1; i <= n; i++)
                factorial *= i;

            return factorial;
        }

        //Вычисление обратного числового значения аргумента
        static double Reciprocal(int n)
        {
            return 1.0 / n;
        }

        //Возврат дробной части числового аргумента
        static decimal FracPart(decimal n)
        {
            return n % 1;
        }
        //Возврат флага четности аргумента
        static bool IsEven(int n)
        {
            return n % 2 == 0;
        }

        //Возврат флага нечетности аргумента
        static bool IsOdd(int n)
        {
            return !IsEven(n);
        }

        //Вычисление кубического корня аргумента
        static double Crt(double n)
        {
            return Math.Pow(n, (1.0/3.0));
        }

        //Вычисление радиан по аргументу в градусах
        static double DegToRad(double n)
        {
            return n * Math.PI / 180;
        }

        //Вычисление градусов по аргументу в радианах
        static double RadToDeg(double n)
        {
            return n * 180 / Math.PI; 
        }
        
        //Проверка числа на принадлежность ряду чисел-степеней двойки
        static bool BinaryDigit(BigInteger bigInteger)
        {
            var result = false;
            var i = 0;
            while(bigInteger >= (int)Math.Pow(2, i))
            {
                if ((int)Math.Pow(2, i++) == bigInteger)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static void Main()
        {
            Console.WriteLine(Fact(10));
            Console.WriteLine(Reciprocal(5));
            Console.WriteLine(FracPart(10.5443m));
            Console.WriteLine(IsEven(5642));
            Console.WriteLine(IsOdd(567));
            Console.WriteLine(Crt(27));
            Console.WriteLine(DegToRad(30));
            Console.WriteLine(RadToDeg(DegToRad(30)).ToString("F2"));
            Console.WriteLine(BinaryDigit(new BigInteger(1024)));
            Console.WriteLine(BinaryDigit(new BigInteger(1025)));

        } 
    }
}

