using System;

namespace LAB3_1
{
    struct Point
    {
        public double x;
        public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Distance(Point point)
        {
            return Math.Sqrt(Math.Pow(x - point.x, 2) + Math.Pow(y - point.y, 2));
        }

        public override string ToString()
        {
            return $"Точка(x:{x},y:{y})";
        }
    }

    struct Line
    {
        public Point point1;
        public Point point2;

        public Line(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public double Length()
        {
            return point1.Distance(point2);
        }

        public bool PointBelongFigure(Point point)
        {
            return 0 == Length().CompareTo(point.Distance(point1) + point.Distance(point2));
        }
        public override string ToString()
        {
            return $"Линия, начало(x:{point1.x},y:{point1.y}), конец(x:{point2.x},y:{point2.y})";
        }
    }

    struct Triangle
    {
        public Point point1;
        public Point point2;
        public Point point3;

        public Triangle(Point point1, Point point2, Point point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }
        public bool PointBelongFigure(Point point)
        {
            var a = (point1.x - point.x) * (point2.y - point1.y) - (point2.x - point1.x) * (point1.y - point.y);
            var b = (point2.x - point.x) * (point3.y - point2.y) - (point3.x - point2.x) * (point2.y - point.y);
            var c = (point3.x - point.x) * (point1.y - point3.y) - (point1.x - point3.x) * (point3.y - point.y);

            return (a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0);
        }

        public override string ToString()
        {
            return $"Треугольник с вершинами: (x:{point1.x},y:{point1.y}), (x:{point2.x},y:{point2.y}), (x:{point3.x}, y:{point3.y})";
        }
    }

    /*
     * point1 ****** point2
     *                    * 
     *                    *
     *                    *
     *                    *
     *                    *
     * point4 ****** point3
     */
    struct Square
    {
        public Point point1;
        public Point point2;
        public Point point3;
        public Point point4;

        public Square(Point point1, Point point2, Point point3, Point point4)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.point4 = point4;
        }

        public bool PointBelongFigure(Point point)
        {
            return point1.x <= point.x && point.x <= point2.x && point2.y >= point.y && point.y >= point3.y;
        }

        public double Area()
        {
            return point1.Distance(point2) * point2.Distance(point3);
        }

        public double Perimeter()
        {
            return 4 * point1.Distance(point2);
        }

        public override string ToString()
        {
            return $"Квадрат с вершинами: (x:{point1.x},y:{point1.y}), (x:{point2.x},y:{point2.y}), (x:{point3.x}, y:{point3.y}), (x:{point4.x}, y:{point4.y})";
        }
    }

    struct Circle
    {
        public Point center;
        public double radius;

        public Circle(double radius, Point center)
        {
            this.center = center;
            this.radius = radius;
        }

        public bool PointBelongFigure(Point point)
        {
            return (Math.Pow(center.x - point.x, 2) + Math.Pow(center.y - point.y, 2) <= Math.Pow(radius, 2));
        }

        public double Area()
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        public double Perimeter()
        {
            return 2 * Math.PI * radius;
        }

        public override string ToString()
        {
            return $"Круг с центром (x:{center.x},y:{center.y}) и радиусом {radius}";
        }
    }
    /*
      * point1 ****** point2
      *                    * 
      *                    *
      * point4 ****** point3
      */
    struct Rectangle
    {
        public Point point1;
        public Point point2;
        public Point point3;
        public Point point4;

        public Rectangle(Point point1, Point point2, Point point3, Point point4)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.point4 = point4;
        }

        public bool PointBelongFigure(Point point)
        {
            return point1.x <= point.x && point.x <= point2.x && point2.y >= point.y && point.y >= point3.y;
        }

        public double Area()
        {
            return point1.Distance(point2) * point2.Distance(point3);
        }

        public double Perimeter()
        {
            return 2 * (point1.Distance(point2) + point2.Distance(point3));
        }

        public override string ToString()
        {
            return $"Прямоугольник с вершинами: (x:{point1.x},y:{point1.y}), (x:{point2.x},y:{point2.y}), (x:{point3.x}, y:{point3.y}), (x:{point4.x}, y:{point4.y})";
        }
    }
    /*
      *      point2
      *    *         * 
      * point1      point3
      *    *         *
      *      point4 
      */
    struct Rhomb
    {
        public Point point1;
        public Point point2;
        public Point point3;
        public Point point4;
        public Point center;

        public Rhomb(Point point1, Point point2, Point point3, Point point4)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.point4 = point4;
            center = new Point((point1.x - point3.x) / 2, (point2.y - point4.y) / 2);
        }

        public bool PointBelongFigure(Point point)
        {
            var triangle1 = new Triangle(point1, point2, point3);
            var triangle2 = new Triangle(point1, point3, point4);

            return triangle1.PointBelongFigure(point) || triangle2.PointBelongFigure(point);
        }

        public double Area()
        {
            return (point1.Distance(point3) + point2.Distance(point4)) / 2;
        }

        public double Perimeter()
        {
            return 4 * point1.Distance(point2);
        }

        public override string ToString()
        {
            return $"Ромб с вершинами: (x:{point1.x},y:{point1.y}), (x:{point2.x},y:{point2.y}), (x:{point3.x}, y:{point3.y}), (x:{point4.x}, y:{point4.y})";
        }
    }

    struct ConveyerControl
    {
        public enum action : int
        {
            старт = 1,
            стоп = 2,
            вперед = 3,
            назад = 4
        };

        public void Conveyer(action act)
        {
            switch (act)
            {
                case (action)1:
                    Console.WriteLine("Запуск");
                    break;
                case (action)2:
                    Console.WriteLine("Остановка");
                    break;
                case (action)3:
                    Console.WriteLine("Перемещение вперед");
                    break;
                case (action)4:
                    Console.WriteLine("Перемещение назад");
                    break;
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var point = new Point(1, 1);
            Console.WriteLine(point);
            Console.WriteLine(point.Distance(new Point(1, 5)));

            var line = new Line(new Point(1, 5), new Point(1, 1));
            Console.WriteLine(line);
            Console.WriteLine(line.PointBelongFigure(new Point(1, 3)));
            Console.WriteLine(line.Length());

            var triangle = new Triangle(new Point(0, 0), new Point(0, 3), new Point(3, 0));
            Console.WriteLine(triangle);
            Console.WriteLine(triangle.PointBelongFigure(new Point(1, 1)));

            var square = new Square(new Point(0, 4), new Point(4, 4), new Point(4, 0), new Point(0, 0));
            Console.WriteLine(square);
            Console.WriteLine(square.PointBelongFigure(new Point(3, 2)));
            Console.WriteLine(square.Area());
            Console.WriteLine(square.Perimeter());

            var circle = new Circle(3, new Point(0, 0));
            Console.WriteLine(circle);
            Console.WriteLine(circle.PointBelongFigure(new Point(1, 2)));
            Console.WriteLine(circle.Area());
            Console.WriteLine(circle.Perimeter());

            var rhomb = new Rhomb(new Point(0, 2), new Point(1, 4), new Point(2, 2), new Point(1, 0));
            Console.WriteLine(rhomb);
            Console.WriteLine(rhomb.PointBelongFigure(new Point(1, 1)));
            Console.WriteLine(rhomb.Area());
            Console.WriteLine(rhomb.Perimeter());

            var conveyerControl = new ConveyerControl();
            Console.WriteLine("Запущен конвеер, управление: ← → ↑ ↓, выход: Enter");
            while (true)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                {
                    break;
                }

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        conveyerControl.Conveyer((ConveyerControl.action)3);
                        break;
                    case ConsoleKey.DownArrow:
                        conveyerControl.Conveyer((ConveyerControl.action)4);
                        break;
                    case ConsoleKey.LeftArrow:
                        conveyerControl.Conveyer((ConveyerControl.action)2);
                        break;
                    case ConsoleKey.RightArrow:
                        conveyerControl.Conveyer((ConveyerControl.action)1);
                        break;
                    default:
                        Console.WriteLine("Неверное действие, для остановки конвеера нажмите Enter");
                        break;
                }
            }
        }
    }
}