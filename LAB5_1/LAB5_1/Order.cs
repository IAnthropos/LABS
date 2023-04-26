using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace LAB5_1
{
    public class Order
    {
        public Client client;
        private List<Item> items;

        private Order(Client client) 
        {
            this.client = client;
            items = new List<Item>();
        }
        public static Order Create(Client client)
        {
            return new Order(client);
        }

        public void GetSum()
        {
            decimal summ = 0;
            foreach (Item item in items)
            {
                summ += item.GetPrice();
            }
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void DeleteItem(int index)
        {
            items.RemoveAt(index);
        }

        public void ShowItems()
        {
            foreach (Item item in items)
            {
                Console.WriteLine(item.GetDescription());
            }
        }
    }
}