using System;
using System.ComponentModel;
using System.Security.Principal;

namespace Hostilities
{
    delegate void MyEventHandler();

    class MyEvent
    {
        MyEventHandler[] evnt = new MyEventHandler[1];
        public event MyEventHandler SomeEvent
        {
            // Добавить событие в список.
            add
            {
                int i;
                for (i = 0; i < 1; i++)
                    if (evnt[i] == null && evnt[i] != value)
                    {
                        evnt[i] = value;
                        break;
                    }
                if (i == 1) Console.WriteLine("Список обработчиков событий заполнен.");
            }
            // Удалить событие из списка.
            remove
            {
                int i;
                for (i = 0; i < 1; i++)
                    if (evnt[i] == value)
                    {
                        evnt[i] = null;
                        break;
                    }
                if (i == 1) Console.WriteLine("Обработчик событий не найден.");
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < 1; i++)
                if (evnt[i] != null) evnt[i]();
        }
    }
    class Opponent
    {
        public string Name { get; set; }
        public MyEvent evt = new MyEvent();

        public Opponent(string name)
        { 
            this.Name = name; 
        
        }
        public void Attack()
        {
            Console.WriteLine($"Аттака {Name}");
            evt.OnAttack();
        }
        
        public void ContrAttack() 
        {
            Console.WriteLine($"Контратака {Name}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var opponent1 = new Opponent("Океании");
            var opponent2 = new Opponent("Остазии");
            opponent1.evt.SomeEvent += opponent2.ContrAttack;
            opponent2.evt.SomeEvent += opponent1.ContrAttack;
            opponent1.Attack();
            opponent2.Attack();
        }

    }
}

