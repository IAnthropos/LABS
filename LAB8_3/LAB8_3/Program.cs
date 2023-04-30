using System;
using System.ComponentModel;
using System.Security.Principal;

namespace PingPong
{
    enum PingPongType
    {
        Ping = 0,
        Pong = 1
    }
    delegate void MyEventHandler(string ip, int size);

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

        public void OnSend(string ip, int size)
        {
            for (int i = 0; i < 1; i++)
                if (evnt[i] != null) evnt[i](ip, size);
        }
    }
    class PingPong
    {
        public PingPongType Type { get; set; }
        public string IP { get; set; }

        public MyEvent evt = new MyEvent();
        

        public PingPong(PingPongType type, string ip)
        {
            Type = type;
            IP = ip;

        }
        public void Send()
        {
            Random rnd = new Random();
            var size = rnd.Next(100, 1024);
            Console.WriteLine($"Отправлено сообщени от {IP} размер {size}");
            evt.OnSend(IP, size);
        }

        public void Receive(string ip, int size)
        {
            if (size > 0)
            {
                Console.WriteLine($"{IP} получил пакет от {ip} размер {size}");
                var answerSize = 0;
                evt.OnSend(IP, answerSize);
            }
            else
            {
                Console.WriteLine($"{IP} получил ответ от {ip}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ping = new PingPong(PingPongType.Ping, "192.168.1.1");
            var pong = new PingPong(PingPongType.Pong, "192.168.1.2");
            ping.evt.SomeEvent += pong.Receive;
            pong.evt.SomeEvent += ping.Receive;
            ping.Send();
            pong.Send();
        }

    }
}

