using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab12
{
    public class Program
    {
        struct MailList
        {
            public int _Index { get; init; }
            public string _Country { get; init; }
            public string _City { get; init; }
            public string _Street { get; init; }
            public int _House { get; init; }
            public MailList(int index, string country, string city, string street, int house)
            {
                _Index = index;
                _Country = country;
                _City = city;
                _Street = street;
                _House = house;
            }
            public override string ToString()
            {
                return $"Почтовый адрес: индекс {_Index}, страна {_Country}, город {_City}, улица {_Street}, дом {_House}";
            }
        }

        class Inventory
        {
            public string _Name { get; set; }
            public string _Description { get; set; }
            public int _Count { get; set; }
            public decimal _Price { get; set; }
            public string _Category { get; set; }
            public Inventory(string name, string description, int count, decimal price, string category) 
            { 
                _Name = name;
                _Description = description;
                _Count = count;
                _Price = price;
                _Category = category;
            }
            public override string ToString()
            {
                return $"Товар: название {_Name}, описание {_Description}, кол. на скаде {_Count}, цена {_Price}, категория {_Category}";
            }
        }

        class SitesUsageLog
        {
            public string _Domain { get; set; }
            public string _IP { get; set; }
            public double _UseDuration { get; set; }
            public double _CiteEntrances { get; set; }
            public double _Users { get; set; }

            public SitesUsageLog(string domain, string iP, double useDuration, double citeEntrances, double users)
            {
                _Domain = domain;
                _IP = iP;
                _UseDuration = useDuration;
                _CiteEntrances = citeEntrances;
                _Users = users;
            }
            public override string ToString()
            {
                return $"Лог сайта: домен {_Domain}, IP {_IP}, кол. времени на сайте {_UseDuration}, кол. входов {_CiteEntrances}, кол. пользователей {_Users}";
            }
        }

        public static void Main(string[] args)
        {
            var mailsArray = FormArrayOfMail();
            
            var moscowMails = from mail in mailsArray
                              where mail._City == "Москва"
                              select mail;

            Console.WriteLine("Адреса в Москве");
            foreach (var mail in moscowMails)
            {
                Console.WriteLine(mail);    
            }

            var minskMails = from mail in mailsArray.AsParallel().WithDegreeOfParallelism(2)
                             where mail._City == "Минск"
                             orderby mail._Street, mail._House
                             select mail;
            
            Console.WriteLine("Адреса в Минске");
            foreach (var mail in minskMails)
            {
                Console.WriteLine(mail);
            }


            var inventory = FormInventoryList();

            var littleStocks = from item in inventory
                             where item._Count < 30
                             orderby item._Category, item._Price descending
                             select item;

            Console.WriteLine("Мало запасов");
            foreach (var item in littleStocks)
            {
                Console.WriteLine(item);
            }


            var bigStocks = from item in littleStocks.AsParallel().AsOrdered()
                            where item._Category == "сиз"
                            select item;

            Console.WriteLine("Мало запасов категории СИЗ");
            foreach (var item in bigStocks)
            {
                Console.WriteLine(item);
            }

            CancellationTokenSource cancelTokSrc = new CancellationTokenSource();

            var stocks = -1;

            var stockValue = from item in littleStocks.AsParallel().AsOrdered().WithCancellation(cancelTokSrc.Token)
                             where item._Count < stocks
                             select item;

            Task cancelTsk = Task.Factory.StartNew(() =>
            {
                if (stocks < 0)
                    cancelTokSrc.Cancel();
            });

            try
            {
                foreach (var item in stockValue)
                {
                    Console.WriteLine(item);
                }
            }
            catch (System.OperationCanceledException)
            {
                Console.WriteLine("Запасы не могут быть меньше нуля");
            }

            var sitesUsageLog = FormSitesLogsArrayList();

            var mostМisitedSites = from SitesUsageLog site in sitesUsageLog
                                   where site._CiteEntrances >= 50000
                                   orderby site._CiteEntrances, site._UseDuration
                                   select site;
            
            Console.WriteLine("Самые посещаемые сайты");
            foreach (var site in mostМisitedSites)
            {
                Console.WriteLine(site);
            }

            var manyUsersSites = from SitesUsageLog site in sitesUsageLog.AsParallel()
                                 where site._CiteEntrances >= 50000
                                 orderby site._CiteEntrances, site._UseDuration
                                 select site;


            Console.WriteLine("Сайты с более чем 1000 пользователей");
            foreach (var site in manyUsersSites)
            {
                Console.WriteLine(site);
            }
        }

        private static MailList[] FormArrayOfMail()
        {
            var mailLIst = new MailList[]
            {
                new MailList( 101010, "РФ", "Москва", "Пушкина", 10),
                new MailList( 101010, "РФ", "Москва", "Пушкина", 11),
                new MailList( 101010, "РФ", "Москва", "Пушкина", 12),
                new MailList( 101020, "РФ", "Москва", "Шверника", 1),
                new MailList( 101020, "РФ", "Москва", "Шверника", 5),
                new MailList( 101020, "РФ", "Москва", "Шверника", 10),
                new MailList( 102020, "РФ", "Калуга", "Советская", 1),
                new MailList( 102020, "РФ", "Калуга", "Советская", 1),
                new MailList( 103020, "РФ", "Воронеж", "Пионерская", 34),
                new MailList( 103020, "РФ", "Воронеж", "Заводская", 56),
                new MailList( 567890, "Норвегия", "Осло", "Первая", 31),
                new MailList( 567890, "Норвегия", "Осло", "Первая", 34),
                new MailList( 202010, "Белоруссия", "Минск", "Победы", 1),
                new MailList( 202010, "Белоруссия", "Минск", "Победы", 2),
                new MailList( 202010, "Белоруссия", "Минск", "Красная", 5),
                new MailList( 202010, "Белоруссия", "Минск", "Советская", 10),
                new MailList( 101020, "РФ", "Москва", "Никольская", 55),
                new MailList( 101030, "РФ", "Москва", "Никольская", 5),
                new MailList( 101030, "РФ", "Москва", "Никольская", 10),
                new MailList( 457778, "Польша", "Варшава", "Новая", 60)
            };
            return mailLIst;
        }

        private static List<Inventory> FormInventoryList()
        {
            var inventoryList = new List<Inventory>()
            {
                new Inventory("Куртка", "Теплая", 100, 10000m, "одежда"),
                new Inventory("Джинсы", "Модные", 10, 1500m, "одежда"),
                new Inventory("Шапка", "Красивая", 20, 500m, "одежда"),
                new Inventory("Носки", "Парные", 200, 100m, "одежда"),
                new Inventory("Шарф", "Длинный", 300, 1000m, "одежда"),
                new Inventory("Очки", "Солнцезащитные", 50, 800m, "аксессуары"),
                new Inventory("Кроссовки", "Спорт", 150, 8000m, "обувь"),
                new Inventory("Тапки", "Домашние", 450, 700m, "обувь"),
                new Inventory("Платки", "Красивые", 50, 1200m, "аксессуары"),
                new Inventory("Зеркало", "Круглое", 500, 500m, "аксессуары"),
                new Inventory("Брелок", "Новинка", 5000, 300m, "аксессуары"),
                new Inventory("Шлем", "Велосипедный", 20, 1500m, "сиз"),
                new Inventory("Сумка", "Мужская", 20, 3500m, "сумки"),
                new Inventory("Рюкзак", "Мужской", 110, 3500m, "сумки"),
                new Inventory("Кошелек", "Мужской", 110, 1500m, "аксессуары"),
                new Inventory("Сумка", "Женская", 200, 7500m, "сумки"),
                new Inventory("Рюкзак", "Женский", 110, 3500m, "сумки"),
                new Inventory("Кошелек", "Женский", 110, 1500m, "аксессуары"),
                new Inventory("Шлем", "Мотоциклетный", 10, 4500m, "сиз"),
                new Inventory("Шлем", "Детский", 10, 4800m, "сиз")
            };

            return inventoryList;
        }

        private static ArrayList FormSitesLogsArrayList()
        {
            var sitesUsageLog = new ArrayList()
            {
                 new SitesUsageLog("yandex.ru", "88.89.32.11", 100000.0, 30000, 5000),
                 new SitesUsageLog("google.com", "43.22.31.15", 1000000.0, 300000, 50000),
                 new SitesUsageLog("youtube.com", "46.226.30.10", 11100000.0, 200000, 46000),
                 new SitesUsageLog("yahoo.com", "46.33.33.30", 10000.0, 2000, 1000),
                 new SitesUsageLog("gmail.com", "46.33.33.30", 100000.0, 20000, 10000),
                 new SitesUsageLog("mail.com", "50.33.50.30", 200000.0, 10000, 20000),
                 new SitesUsageLog("netflix.com", "67.255.33.30", 20000.0, 10000, 10000),
                 new SitesUsageLog("netflix.kz", "14.0.1.30", 50000.0, 1000, 100),
                 new SitesUsageLog("food.com", "43.255.00.30", 1500, 10, 5),
                 new SitesUsageLog("health.com", "43.254.00.01", 1500, 10, 6),
                 new SitesUsageLog("book.com", "89.87.00.01", 15000, 1000, 600),
                 new SitesUsageLog("book.ru", "10.87.00.10", 150, 10, 1),
                 new SitesUsageLog("book.com", "10.87.00.10", 1500, 100, 10),
                 new SitesUsageLog("ya.com", "15.15.15.10", 15000, 5000, 550),
                 new SitesUsageLog("ya.org", "15.15.16.10", 0, 0, 0),
                 new SitesUsageLog("me.me", "15.15.20.10", 1110, 11, 11),
                 new SitesUsageLog("music.org", "15.15.14.10", 43352, 25223, 1331),
                 new SitesUsageLog("no.org", "43.43.14.10", 56664,2121, 1231),
                 new SitesUsageLog("cats.com", "88.88.88.77", 5660640,212100, 123100),
                 new SitesUsageLog("clck.cru", "10.111.78.1", 334343, 32323, 12000)
            };

            return sitesUsageLog;
        }
    }
}
