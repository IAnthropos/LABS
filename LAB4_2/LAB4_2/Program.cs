using System;

namespace LAB4_2
{
    class Programm
    {
        class Deposit
        {
            private static double ratePerYear;
            private static string bankName;
            private static int periodYears;
            private double summ;
            private string fullName;
            private DateOnly startDate;

            static Deposit()
            {
                ratePerYear = 0.10;
                bankName = "ООО БАНК";
                periodYears = 1; 
            }

            public Deposit(double summ, string fullName, DateOnly startDate)
            {
                this.summ = summ;
                this.fullName = fullName;
                this.startDate = startDate;
            }

            public override string ToString()
            {
                return $"{bankName}, ставка: {ratePerYear}, минимальный период (в годах):" +
                       $"{periodYears}";
            }

            public string ClientInfo()
            {
                return $"Клиент: {fullName}, сумма: {summ.ToString("F2")}, дата начала вклада: {startDate}";
            }

            public string SumPerDays(int days)
            {
                var depositSumm = summ + (summ * ratePerYear / 365) * days;
                return $"Вклад через {days} дней: {depositSumm.ToString("F2")}";
            }
        }

        public static void Main()
        {
            var deposit = new Deposit(10000, "Иван Иванов", new DateOnly(2022, 10, 01));
            Console.WriteLine(deposit);
            Console.WriteLine(deposit.ClientInfo());
            Console.WriteLine(deposit.SumPerDays(251));
        }
    }
}

