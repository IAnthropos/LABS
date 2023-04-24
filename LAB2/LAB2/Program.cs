using System;

namespace LAB2
{
    internal class Program
    {
        class Deposit
        {
            public decimal Balance
            { get; set; }

            public double Percent
            { get; set; }

            private int DepositTerm
            { get; set; }

            public Deposit(decimal initBalance, double percent, int depostiTerm)
            {
                Balance = Math.Round(initBalance, 3);
                Percent = percent;
                DepositTerm = depostiTerm;

                if (Balance < 5000)
                {
                    Console.WriteLine("Первоначальный остаток меньше 5000");
                }

                try
                {
                    if (Balance == 0)
                    {
                        throw new Exception();
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("0 баланс дает нулевую прибыль");
                    return;
                }

            }
            public void ShowContributionPerYear()
            {
                decimal newBalance = Balance;

                for (int i = 0; i < DepositTerm; i++)
                {
                    var profit = Math.Round(newBalance * Convert.ToDecimal(Percent), 3);
                    newBalance += profit;
                    Console.WriteLine($"Год {i + 1}. Вклад {newBalance}, прибыль {profit}");
                }
                Balance = newBalance;
            }
            public void ShowContibutionFromStock()
            {
                decimal newBalance = Balance;

                for (int x = 1; x <= 12; x++)
                {
                    var interestRate = Math.Round(0.1 + 0.02 * x * x + 0.5 * Math.Sin(2 * x) + Math.Cos(3 * x), 3);
                    var profit = Math.Round(newBalance * Convert.ToDecimal(interestRate / 100), 3);
                    newBalance += profit;
                    Console.WriteLine($"Месяц {x}. Вклад {newBalance}, прибыль {profit}, процент {interestRate}");
                }
            }
            public void ShowContibutionFromStock(int months)
            {
                decimal newBalance = Balance;
                try
                {
                    if (months < 13 || months > 48)
                    {
                        throw new Exception();
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("Расчет возможен только в диапазоне 13-48 месяцев");
                    return;
                }

                for (int x = 1; x <= months; x++)
                {
                    var interestRate = Math.Round(0.1 + 0.02 * x * x + 0.5 * Math.Sin(2 * x) + Math.Cos(3 * x), 3);
                    var profit = Math.Round(newBalance * Convert.ToDecimal(interestRate / 100), 3);
                    newBalance += profit;
                    Console.WriteLine($"Месяц {x}. Вклад {newBalance}, прибыль {profit}, процент {interestRate}");
                }
            }

            public override string ToString()
            {
                return $"Баланс: {Balance}, процент: {Percent}, срок вклада: {DepositTerm}";
            }
        }

        public static void Main()
        {

            DoTask3(DoTask2(DoTask1()));
        }

        static Deposit DoTask1()
        {
            Console.Write("Введите сумма первоначального вклада: ");
            decimal.TryParse(Console.ReadLine(), out decimal balance);

            Deposit deposit = new Deposit(balance, 0.08, 10);
            Console.WriteLine(deposit);
            deposit.ShowContributionPerYear();
            return deposit;
        }

        static Deposit DoTask2(Deposit deposit)
        {
            deposit.ShowContibutionFromStock();
            return deposit;
        }
        static void DoTask3(Deposit deposit)
        {
            Console.Write("Введите колтчество месяцев: ");
            int.TryParse(Console.ReadLine(), out int months);
            deposit.ShowContibutionFromStock(months);
        }
    }
}

