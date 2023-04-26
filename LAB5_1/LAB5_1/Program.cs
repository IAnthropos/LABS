using LAB5_1;
using System;

internal class Program
{
    public static void Main()
    {
        var client = new Client("Иванов Иван Иванович", "г. Москва, ул. Шверника, д. 1");
        var item1 = new Item("Диван", 100000m, 1, 150, 200, 210);
        var item2 = new Item("Кресло", 10000m, 1, 150, 100, 105);
        var order = Order.Create(client);
        order.AddItem(item1);
        order.AddItem(item2);
        order.AddItem(item2);
        order.DeleteItem(2);
        Console.WriteLine(order.client);
        order.ShowItems();
    }
}