using System;
namespace LAB3_2
{
	public enum RightsToManage : int
	{
		none = 0,
		car = 1,
		boat = 2,
		airoplane = 3
	}

	public abstract class Vehicle
	{
        public double Fuel { get; set; }
		public double PowerReserve { get; set; }
		public RightsToManage RightsToManage { get; set; }
		public double Wear { get; set; }

        public virtual void drive(double hours) { }
    }

	public class Carriage : Vehicle
	{
		public Carriage(double powerReserve, double wear)
		{
            Fuel = 0;
            PowerReserve = powerReserve;
            RightsToManage = RightsToManage.none;
            Wear = wear;
        }

		public override void drive(double hours)
        {
			for (int i = 0; i < hours; i++)
			{
				PowerReserve -= 1.0;
				if (PowerReserve <= 0)
				{
					PowerReserve = 0;
					Console.WriteLine("Лошади устали");
				}
				Wear += 0.1;

				if (Wear >= 100)
				{
					Wear = 100;
					Console.WriteLine("Повозка сломалась");
				}
			}
        }

        public override string ToString()
        {
            return $"Гужевая повозка, права не нужны, запас энергии {PowerReserve}, износ {Wear.ToString("F2")}";
        }
    }

	public class Automobile : Vehicle
	{
        public Automobile(double fuel, double wear)
        {
            Fuel = fuel;
            PowerReserve = 0;
            RightsToManage = RightsToManage.car;
            Wear = wear;
        }

        public override void drive(double hours)
        {
            for (int i = 0; i < hours; i++)
            {
                Fuel -= 10.0;
                if (Fuel <= 0)
                {
                    Fuel = 0;
                    Console.WriteLine("Кончилось топливо");
                }
                Wear += 0.1;

                if (Wear >= 100)
                {
                    Wear = 100;
                    Console.WriteLine("Машина сломалась");
                }
            }
        }

        public override string ToString()
        {
            return $"Автомобиль, нужны првава на управление автомобилем, запас топлива {Fuel}, износ {Wear.ToString("F2")}";
        }
    }

    public class Airplane : Vehicle
    {
        public Airplane(double fuel, double wear)
        {
            Fuel = fuel;
            PowerReserve = 0;
            RightsToManage = RightsToManage.airoplane;
            Wear = wear;
        }

        public override void drive(double hours)
        {
            for (int i = 0; i < hours; i++)
            {
                Fuel -= 100.0;
                if (Fuel <= 0)
                {
                    Fuel = 0;
                    Console.WriteLine("Кончилось топливо");
                }
                Wear += 0.1;

                if (Wear >= 100)
                {
                    Wear = 100;
                    Console.WriteLine("Самолет неисправен");
                }
            }
        }

        public override string ToString()
        {
            return $"Самолет, нужны првава на управление воздушным судном, запас топлива {Fuel}, износ {Wear.ToString("F2")}";
        }
    }

    public class Boat : Vehicle
    {
        public Boat(double fuel, double wear)
        {
            Fuel = fuel;
            PowerReserve = 0;
            RightsToManage = RightsToManage.car;
            Wear = wear;
        }

        public override void drive(double hours)
        {
            for (int i = 0; i < hours; i++)
            {
                Fuel -= 20.0;
                if (Fuel <= 0)
                {
                    Fuel = 0;
                    Console.WriteLine("Кончилось топливо");
                }
                Wear += 0.1;

                if (Wear >= 100)
                {
                    Wear = 100;
                    Console.WriteLine("Катер неисправен");
                }
            }
        }

        public override string ToString()
        {
            return $"Катер, нужны првава на управление катером, запас топлива {Fuel}, износ {Wear.ToString("F2")}";
        }
    }
}

