using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAB5_1
{
    public class Item
    {
        private string name;
        private decimal price;
        private int width;
        private int height;
        private int weight;
        private int length;

        public Item (string name, decimal price, int width, int height, int weight, int length)
        {
            this.name = name;
            this.price = price;
            this.width = width;
            this.height = height;
            this.weight = weight;
            this.length = length;
        }

        public decimal GetPrice()
        {
            return price;
        }

        public string GetName()
        {
            return name;
        }

        public string GetDescription()
        {
            return $"Товар {name}, вес {weight} кг, габариты {length}x{width}x{height} см, цена {price} руб.";
        }
    }
}