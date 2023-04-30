using System;

static class ActionPredicateFunc
{

    static readonly string constString = "const";
    static readonly double constDouble = 10.0;
    static void DoOperation(double a, Action<double> op) => op(a);
    static void Sin(double a) => Console.WriteLine($"Синус угла {a} = {Math.Sin(a)}");
    static void Cos(double a) => Console.WriteLine($"Косинус угла {a} = {Math.Cos(a)}");
    static void Tg(double a)  => Console.WriteLine($"Тангенс угла {a} = {Math.Tan(a)}");

    static Predicate<string> EqualConstStr = (string str) => str.CompareTo(constString) == 0;
    static Predicate<double> EqualConstDouble = (double val) => val == constDouble;

    static Predicate<string> OverConstStr = (string str) => str.CompareTo(constString) > 0;
    static Predicate<double> OverConstDouble = (double val) => val > constDouble;

    static Predicate<string> UnderConstStr = (string str) => str.CompareTo(constString) < 0;
    static Predicate<double> UnderConstDouble = (double val) => val < constDouble;

    static Predicate<string> OverOrEqualConstStr = (string str) => str.CompareTo(constString) > 0 || str.CompareTo(constString) == 0;
    static Predicate<double> OverOrEqualConstDouble = (double val) => val > constDouble || val == constDouble;

    static Predicate<string> UnderOrEqualConstStr = (string str) => str.CompareTo(constString) < 0 || str.CompareTo(constString) == 0;
    static Predicate<double> UnderOrEqualConstDouble = (double val) => val < constDouble || val == constDouble;

    static Predicate<string> NotEqualConstStr = (string str) => str.CompareTo(constString) != 0;
    static Predicate<double> NotEqualConstDouble = (double val) => val != constDouble;

    static bool DoOperation(int n, Func<int, bool> operation) => operation(n);
    static bool DoOperation(decimal n, Func<decimal, bool> operation) => operation(n);

    static Func<int, bool> IsOdd = (x) => x % 2 == 0;
    static Func<int, bool> IsEven = (x) => x % 2 != 0;
    static Func<decimal, bool> IsOddDec = (x) => (int)x % 2 == 0;
    static Func<decimal, bool> IsEvenDec = (x) => (int)x % 2 != 0;

    public static void Main()
    {

        //ACTION
        DoOperation(10, Sin);        
        DoOperation(10, Cos);
        DoOperation(10, Tg);
        //PREDICATE
        Console.WriteLine(EqualConstStr("const"));
        Console.WriteLine(EqualConstDouble(10.0));

        Console.WriteLine(OverConstStr("constt"));
        Console.WriteLine(OverConstDouble(11.0));

        Console.WriteLine(UnderConstStr("cons"));
        Console.WriteLine(UnderConstDouble(9.0));

        Console.WriteLine(OverOrEqualConstStr("const"));
        Console.WriteLine(OverOrEqualConstDouble(10.0));

        Console.WriteLine(UnderOrEqualConstStr("cons"));
        Console.WriteLine(UnderOrEqualConstDouble(9.0));

        Console.WriteLine(NotEqualConstStr("con"));
        Console.WriteLine(NotEqualConstDouble(9.0));

        //FUNC
        Console.WriteLine(DoOperation(6, IsOdd));
        Console.WriteLine(DoOperation(7, IsEven));
        Console.WriteLine(DoOperation(8.1m, IsOddDec));
        Console.WriteLine(DoOperation(9.1m, IsEvenDec));
    }
}