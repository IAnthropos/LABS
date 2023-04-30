using System;

namespace BullsAndBears
{

    enum MarketPlayerType
    {
        Bull = 0,
        Bear = 1
    }

    delegate void MyEventHandler();

    class MyEvent
    {
        MyEventHandler[] evnt = new MyEventHandler[2];
        public event MyEventHandler SomeEvent
        {
            // Добавить событие в список.
            add
            {
                int i;
                for (i = 0; i < 2; i++)
                    if (evnt[i] == null && evnt[i] != value)
                    {
                        evnt[i] = value;
                        break;
                    }
                if (i == 2) Console.WriteLine("Список обработчиков событий заполнен.");
            }
            // Удалить событие из списка.
            remove
            {
                int i;
                for (i = 0; i < 2; i++)
                    if (evnt[i] == value)
                    {
                        evnt[i] = null;
                        break;
                    }
                if (i == 2) Console.WriteLine("Обработчик событий не найден.");
            }
        }

        public void OnChange()
        {
            for (int i = 0; i < 2; i++)
                if (evnt[i] != null) evnt[i]();
        }
    }

    class MarketPlayer
    {
        public MarketPlayerType Type { get; set; }

        public MarketPlayer(MarketPlayerType type)
        {
            this.Type = type;

        }
        public void ChangeHadler()
        {
            switch (Type)
            {
                case MarketPlayerType.Bull:
                    Console.WriteLine("Быки играют на бирже на повышение");
                    break;
                case MarketPlayerType.Bear:
                    Console.WriteLine("Медвевли играют на бирже на понижение");
                    break;
            }
        }

    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            MyEvent evt = new MyEvent();
            MarketPlayer bears = new MarketPlayer(MarketPlayerType.Bear);
            MarketPlayer bulls = new MarketPlayer(MarketPlayerType.Bull);
            evt.SomeEvent += bears.ChangeHadler;
            evt.SomeEvent += bulls.ChangeHadler;
            evt.OnChange();
        }
       
    }
}

    




