using System;

namespace LAB3_2
{
    public class Building
    {
        public int Floors { set; get; }
        public double TotalArea { set; get; }
        public int Residents { set; get; }

        public Building(int floors, double totalArea, int residents)
        {
            Floors = floors;
            TotalArea = totalArea;
            Residents = residents;
        }

        public string BuildingType()
        {
            if (Residents == 0)
            {
                return "Офис";
            }
            else
            {
                return "Дом";
            }
        }

        public override string ToString()
        {
            return $"Тип: {BuildingType()}, количество этажей {Floors}, " +
                   $"общая площадь {TotalArea}, количество жильцов {Residents}";

        }
    }
}

