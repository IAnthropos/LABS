using System;
using System.Drawing;

namespace LAB3_2
{
    class Point
    {
        private static int count; 
        public double X { set; get; }
        public double Y { set; get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
            count++;
        }

        ~Point()
        {
            count--;
        }

        public static void PrintCount()
        {
            Console.WriteLine($"Количество используемых точек: {count}");
        }

        public double Distance(Point point)
        {
            return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
        }

        public override string ToString()
        {
            return $"Точка(x:{X},y:{Y})";
        }
    }

    class Line
    {
        public Point Point1 { set; get; }
        public Point Point2 { set; get; }

        public Line(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public double Length()
        {
            return Point1.Distance(Point2);
        }

        public bool PointBelongFigure(Point point)
        {
            return 0 == Length().CompareTo(point.Distance(Point1) + point.Distance(Point2));
        }
        public override string ToString()
        {
            return $"Линия, начало(x:{Point1.X},y:{Point1.Y}), конец(x:{Point2.X},y:{Point2.Y})";
        }
    }

    struct Triangle
    {
        public Point Point1 { set; get; }
        public Point Point2 { set; get; }
        public Point Point3 { set; get; }

        public Triangle(Point point1, Point point2, Point point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

        public bool PointBelongFigure(Point point)
        {
            var a = (Point1.X - point.X) * (Point2.Y - Point1.Y) - (Point2.X - Point1.X) * (Point1.Y - point.Y);
            var b = (Point2.X - point.X) * (Point3.Y - Point2.Y) - (Point3.X - Point2.X) * (Point2.Y - point.Y);
            var c = (Point3.X - point.X) * (Point1.Y - Point3.Y) - (Point1.X - Point3.X) * (Point3.Y - point.Y);

            return (a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0);
        }

        public override string ToString()
        {
            return $"Треугольник с вершинами: (x:{Point1.X},y:{Point1.Y}), (x:{Point2.X},y:{Point2.Y}), (x:{Point3.X}, y:{Point3.Y})";
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
    class Square
    {
        public Point Point1 { set; get; }
        public Point Point2 { set; get; }
        public Point Point3 { set; get; }
        public Point Point4 { set; get; }

        public Square(Point point1, Point point2, Point point3, Point point4)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Point4 = point4;
        }

        public bool PointBelongFigure(Point point)
        {
            return Point1.X <= point.X && point.X <= Point2.X && Point2.Y >= point.Y && point.Y >= Point3.Y;
        }

        public double Area()
        {
            return Point1.Distance(Point2) * Point2.Distance(Point3);
        }

        public double Perimeter()
        {
            return 4 * Point1.Distance(Point2);
        }

        public override string ToString()
        {
            return $"Квадрат с вершинами: (x:{Point1.X},y:{Point1.Y}), (x:{Point2.X},y:{Point2.Y}), (x:{Point3.X}, y:{Point3.Y}), (x:{Point4.X}, y:{Point4.Y})";
        }
    }

    class Circle
    {
        public Point Center { set; get; }
        public double Radius { set; get; }

        public Circle(double radius, Point center)
        {
            Center = center;
            Radius = radius;
        }

        public bool PointBelongFigure(Point point)
        {
            return (Math.Pow(Center.X - point.X, 2) + Math.Pow(Center.Y - point.Y, 2) <= Math.Pow(Radius, 2));
        }

        public double Area()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return $"Круг с центром (x:{Center.X},y:{Center.Y}) и радиусом {Radius}";
        }
    }
    /*
      * point1 ****** point2
      *                    * 
      *                    *
      * point4 ****** point3
      */
    class Rectangle
    {
        public Point Point1 { set; get; }
        public Point Point2 { set; get; }
        public Point Point3 { set; get; }
        public Point Point4 { set; get; }

        public Rectangle(Point point1, Point point2, Point point3, Point point4)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Point4 = point4;
        }

        public bool PointBelongFigure(Point point)
        {
            return Point1.X <= point.X && point.X <= Point2.X && Point2.Y >= point.Y && point.Y >= Point3.Y;
        }

        public double Area()
        {
            return Point1.Distance(Point2) * Point2.Distance(Point3);
        }

        public double Perimeter()
        {
            return 2 * (Point1.Distance(Point2) + Point2.Distance(Point3));
        }

        public override string ToString()
        {
            return $"Прямоугольник с вершинами: (x:{Point1.X},y:{Point1.Y}), (x:{Point2.X},y:{Point2.Y}), (x:{Point3.X}, y:{Point3.Y}), (x:{Point4.X}, y:{Point4.Y})";
        }
    }
    /*
      *      point2
      *    *         * 
      * point1      point3
      *    *         *
      *      point4 
      */
    class Rhomb
    {
        public Point Point1 { set; get; }
        public Point Point2 { set; get; }
        public Point Point3 { set; get; }
        public Point Point4 { set; get; }
        public Point Center { set; get; }

        public Rhomb(Point point1, Point point2, Point point3, Point point4)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Point4 = point4;
            Center = new Point((Point1.X - Point3.X) / 2, (Point2.Y - Point4.Y) / 2);
        }

        public bool PointBelongFigure(Point point)
        {
            var triangle1 = new Triangle(Point1, Point2, Point3);
            var triangle2 = new Triangle(Point1, Point3, Point4);

            return triangle1.PointBelongFigure(point) || triangle2.PointBelongFigure(point);
        }

        public double Area()
        {
            return (Point1.Distance(Point3) + Point2.Distance(Point4)) / 2;
        }

        public double Perimeter()
        {
            return 4 * Point1.Distance(Point2);
        }

        public override string ToString()
        {
            return $"Ромб с вершинами: (x:{Point1.X},y:{Point1.Y}), (x:{Point2.X},y:{Point2.Y}), (x:{Point3.X}, y:{Point3.Y}), (x:{Point4.X}, y:{Point4.Y})";
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var point = new Point(1, 1);
            Console.WriteLine(point);
            Console.WriteLine(point.Distance(new Point(1, 5)));
            Point.PrintCount();

            var line = new Line(new Point(1, 5), new Point(1, 1));
            Console.WriteLine(line);
            Console.WriteLine(line.PointBelongFigure(new Point(1, 3)));
            Console.WriteLine(line.Length());
            Point.PrintCount();

            var triangle = new Triangle(new Point(0, 0), new Point(0, 3), new Point(3, 0));
            Console.WriteLine(triangle);
            Console.WriteLine(triangle.PointBelongFigure(new Point(1, 1)));
            Point.PrintCount();

            var square = new Square(new Point(0, 4), new Point(4, 4), new Point(4, 0), new Point(0, 0));
            Console.WriteLine(square);
            Console.WriteLine(square.PointBelongFigure(new Point(3, 2)));
            Console.WriteLine(square.Area());
            Console.WriteLine(square.Perimeter());
            Point.PrintCount();

            var circle = new Circle(3, new Point(0, 0));
            Console.WriteLine(circle);
            Console.WriteLine(circle.PointBelongFigure(new Point(1, 2)));
            Console.WriteLine(circle.Area());
            Console.WriteLine(circle.Perimeter());
            Point.PrintCount();

            var rhomb = new Rhomb(new Point(0, 2), new Point(1, 4), new Point(2, 2), new Point(1, 0));
            Console.WriteLine(rhomb);
            Console.WriteLine(rhomb.PointBelongFigure(new Point(1, 1)));
            Console.WriteLine(rhomb.Area());
            Console.WriteLine(rhomb.Perimeter());
            Point.PrintCount();
        }
    }
}

